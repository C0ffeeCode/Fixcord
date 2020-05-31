using Discord.WebSocket;
using Fixcord.Uwp.Pages;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp.Controls
{
	public sealed partial class ChannelSelector : UserControl
	{
		public ChannelSelector()
		{
			this.InitializeComponent();
			ClientBot.SelectedGuildChanged += ClientBot_SelectedGuildChanged;
		}

		private void ClientBot_SelectedGuildChanged()
		{
			if (ClientBot.SelectedGuild != null)
				ChannelList.ItemsSource = ClientBot.SelectedGuild.Channels.AsEnumerable()
					.Where(c => c.GetType().Name.ToString() == "SocketTextChannel");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (ClientBot.SelectedGuild != null)
			{
				ChannelList.ItemsSource = ClientBot.SelectedGuild.Channels.AsEnumerable()
					.Where(c => c.GetType().Name.ToString() == "SocketTextChannel");
			};
		}

		private void ChannelList_SelectionChanged(object sender, RoutedEventArgs e)
		{
			if (ChannelList.SelectedItem == null)
				return;

			if (Window.Current.CoreWindow.GetKeyState(VirtualKey.Control).
				HasFlag(CoreVirtualKeyStates.Down) | Configuration.multiWinModeEnabled)
				OpenInNewWindow((SocketTextChannel)ChannelList.SelectedItem);
			else ClientBot.SelectedTextChannel = (SocketTextChannel)ChannelList.SelectedItem;

			//var channelTypeName = ChannelList.SelectedItem.GetType().Name;
			//switch (channelTypeName)
			//{
			//	case ("SocketVoiceChannel"):
			//		ClientBot.SelectedVoiceChannel = (SocketVoiceChannel)ChannelList.SelectedItem;
			//		break;
			//	case ("SocketTextChannel"):
			//		ClientBot.SelectedTextChannel = (SocketTextChannel)ChannelList.SelectedItem;
			//		break;
			//	default:
			//		Debug.WriteLine($"Unknown channel type selected: {channelTypeName}");
			//		break;
			//}
		}

		private async void OpenInNewWindow(SocketTextChannel chan)
		{
			AppWindow appWindow = await AppWindow.TryCreateAsync();
			appWindow.Title = chan.Name;

			Frame appWindowContentFrame = new Frame();
			appWindowContentFrame.Navigate(typeof(ChatPage));
			((ChatPage)appWindowContentFrame.Content).Channel = chan;

			ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
			await appWindow.TryShowAsync();
			appWindow.Closed += delegate
			{
				appWindowContentFrame.Content = null;
				appWindow = null;
			};
		}
	}
}

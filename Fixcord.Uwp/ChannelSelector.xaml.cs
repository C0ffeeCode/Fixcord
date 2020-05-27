using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Diagnostics;
using Discord.WebSocket;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp
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
			{
				ChannelList.ItemsSource = ClientBot.SelectedGuild.Channels.AsEnumerable()
					.Where(c => c.GetType().Name.ToString() == "SocketTextChannel");
			};
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
			{
				return;
			}

			var channelTypeName = ChannelList.SelectedItem.GetType().Name;
			switch (channelTypeName)
			{
				case ("SocketVoiceChannel"):
					ClientBot.SelectedVoiceChannel = (SocketVoiceChannel)ChannelList.SelectedItem;
					break;
				case ("SocketTextChannel"):
					ClientBot.SelectedTextChannel = (SocketTextChannel)ChannelList.SelectedItem;
					break;
				default:
					Debug.WriteLine($"Unknown channel type selected: {channelTypeName}");
					break;
			}
		}
	}
}

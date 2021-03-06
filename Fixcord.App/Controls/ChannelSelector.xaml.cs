using Discord.WebSocket;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fixcord.App.Controls
{
	/// <summary>
	/// Interaction logic for ChannelSelector.xaml
	/// </summary>
	public partial class ChannelSelector : UserControl
	{
		public ChannelSelector()
		{
			InitializeComponent();
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

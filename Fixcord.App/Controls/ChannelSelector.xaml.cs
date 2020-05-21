using Discord.WebSocket;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fixcord.App
{
	/// <summary>
	/// Interaction logic for ChannelSelector.xaml
	/// </summary>
	public partial class ChannelSelector : UserControl
	{
		public ChannelSelector()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (ClientBot.selectedGuild != null)
			{
				ChannelList.ItemsSource = ClientBot.selectedGuild.Channels.AsEnumerable()
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
					ClientBot.selectedVoiceChannel = (SocketVoiceChannel)ChannelList.SelectedItem;
					break;
				case ("SocketTextChannel"):
					ClientBot.selectedTextChannel = (SocketTextChannel)ChannelList.SelectedItem;
					break;
				default:
					Debug.WriteLine($"Unknown channel type: {channelTypeName}");
					break;
			}
		}
	}
}

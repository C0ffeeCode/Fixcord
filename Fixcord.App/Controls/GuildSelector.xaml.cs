using Discord.WebSocket;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fixcord.App
{
	public partial class GuildSelector : UserControl
	{
		public GuildSelector()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			GuildsList.ItemsSource = ClientBot.client?.Guilds.AsEnumerable();
		}

		private void GuildsList_Selected(object sender, RoutedEventArgs e)
		{
			var selected = (SocketGuild)GuildsList.SelectedItem;
			//Init._client.GetChannel(selected)
			ClientBot.SelectedGuild = selected;
		}
	}
}

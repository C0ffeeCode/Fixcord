using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Fixcord.App
{
	public partial class GuildSelector : UserControl
	{
		public GuildSelector()
		{
			InitializeComponent();
		}

		private Task Refresh()
		{
			GuildsList.ItemsSource = ClientBot.client?.Guilds.AsEnumerable();
			return Task.CompletedTask;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
			=> Refresh();

		private void GuildsList_Selected(object sender, RoutedEventArgs e)
		{
			var selected = (SocketGuild)GuildsList.SelectedItem;
			ClientBot.SelectedGuild = selected;
		}
	}
}

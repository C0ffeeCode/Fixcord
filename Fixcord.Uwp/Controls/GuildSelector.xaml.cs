using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp.Controls
{
	public sealed partial class GuildSelector : UserControl
	{
		public GuildSelector()
		{
			this.InitializeComponent();
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

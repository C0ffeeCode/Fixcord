using Discord.WebSocket;
using Fixcord.Uwp.Pages;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp.Controls
{
	public sealed partial class GuildSelector : UserControl
	{
		public GuildSelector()
			=> InitializeComponent();

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

		private async void Settings_Clicked(object sender, RoutedEventArgs e)
		{
			AppWindow appWindow = await AppWindow.TryCreateAsync();
			appWindow.Title = "Settings";
			appWindow.RequestSize(new Size(200, 200));

			Frame appWindowContentFrame = new Frame();
			appWindowContentFrame.Navigate(typeof(SettingsPage));

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

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fixcord.Uwp.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SettingsPage : Page
	{
		public SettingsPage()
		{
			InitializeComponent();
			tokenInput.Password = Configuration.Token;
			MultiWinModeEnabled.IsChecked = Configuration.multiWinModeEnabled;
		}

		private void TokenInput_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter) Save();
		}

		private void Save_Click(object sender, RoutedEventArgs e)
			=> Save();

		private void Save()
		{
			if (tokenInput.Password != null)
			{
				Configuration.Token = tokenInput.Password;

				ClientBot.client.Dispose();
				new ClientBot().Initialize();
			}

			Configuration.multiWinModeEnabled = (bool)MultiWinModeEnabled.IsChecked;
		}
	}
}

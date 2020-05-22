using Discord;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Fixcord.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ClientBot x = new ClientBot();

		public MainWindow()
		{
			x.Initialize(Configuration.Config.Token!);
			InitializeComponent();
		}

		private void TokenInput_KeyDown(object sender, KeyEventArgs e)
		{
			if( e.Key == Key.Enter)
			{
				Configuration.Config.Token = tokenInput.Text;
				Configuration.Save();
				ClientBot.client!.Dispose();
				x.Initialize(Configuration.Config.Token!);
			}
		}

		private void MessageInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (ClientBot.SelectedTextChannel == null) return;
			if (e.Key == Key.Return)
			{
				((IMessageChannel)ClientBot.SelectedTextChannel!)
					.SendMessageAsync(messageInput.Text);
				messageInput.Text = null;
			}
		}
	}
}

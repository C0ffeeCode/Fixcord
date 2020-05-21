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
		public MainWindow()
		{
			var x = new ClientBot();
			x.Initialize(Configuration.Config.Token!);
			InitializeComponent();
		}

		private void TokenInput_KeyDown(object sender, KeyEventArgs e)
		{
			Configuration.Config.Token = tokenInput.Text;
			Configuration.Save();
		}

		private void MessageInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (ClientBot.SelectedTextChannel == null) return;
			if (e.Key == Key.Return)
			{
				var b = (IMessageChannel)ClientBot.SelectedTextChannel!;
				b.SendMessageAsync(messageInput.Text);
				messageInput.Text = null;
			}
		}
	}
}

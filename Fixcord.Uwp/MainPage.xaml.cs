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
using Discord;
using Windows.System;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Fixcord.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
	{
		readonly ClientBot x = new ClientBot();

		public MainPage()
		{
			Init();
			InitializeComponent();
		}

		private void Init()
		{
			x.Initialize();
		}

		private void TokenInput_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
			{
				Configuration.Token = tokenInput.Text;
				ClientBot.client.Dispose();
				x.Initialize();
			}
		}

		private void MessageInput_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (ClientBot.SelectedTextChannel == null)
			{
				return;
			}

			if (e.Key == VirtualKey.Enter)
			{
				((IMessageChannel)ClientBot.SelectedTextChannel)
					.SendMessageAsync(messageInput.Text);
				messageInput.Text = null;
			}
		}
	}
}

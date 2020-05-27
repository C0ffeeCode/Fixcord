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
using Discord.WebSocket;
using System.Threading.Tasks;
using Windows.UI.Core;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp
{
	public sealed partial class ChatControl : UserControl
	{
		public ChatControl()
		{
			this.InitializeComponent();
			ClientBot.client.MessageReceived += Refresh;
			ClientBot.SelectedTextChannelChanged += () => Refresh();
		}

		private async Task Refresh(SocketMessage m = null)
		{
			if (ClientBot.SelectedTextChannel == null)
				return /*Task.CompletedTask*/;

			await Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
			messagelisttest.ItemsSource = 
				(ClientBot.SelectedTextChannel.GetMessagesAsync()
					.ToListAsync().AsTask().Result)[1]
					.AsEnumerable().OrderBy(s => s.Timestamp)
			);

			//return Task.CompletedTask;
		}
	}
}

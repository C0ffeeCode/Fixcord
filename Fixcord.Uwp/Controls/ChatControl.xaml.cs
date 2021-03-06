using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp.Controls
{
	public sealed partial class ChatControl : UserControl
	{
		public ChatControl()
		{
			InitializeComponent();
			ClientBot.client.MessageReceived += Refresh;
			ClientBot.SelectedTextChannelChanged += () => Refresh();
		}

		private async Task Refresh(SocketMessage m = null)
		{
			if (ClientBot.SelectedTextChannel == null)
				return;

			if (m != null && m.Channel != ClientBot.SelectedTextChannel)
				return;


			await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
			messagelist.ItemsSource =
				(ClientBot.SelectedTextChannel.GetMessagesAsync()
					.ToListAsync().AsTask().Result)[1]
					.AsEnumerable().OrderBy(s => s.Timestamp)
			);

			//messagelist.ScrollIntoView(messagelist.Items.Last());
		}

		private void MessageInput_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (ClientBot.SelectedTextChannel == null)
				return;

			if (e.Key == VirtualKey.Enter)
			{
				((IMessageChannel)ClientBot.SelectedTextChannel)
					.SendMessageAsync(messageInput.Text);
				messageInput.Text = "";
			}
		}
	}
}

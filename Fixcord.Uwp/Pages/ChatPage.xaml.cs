using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fixcord.Uwp.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ChatPage : Page
	{
		public ChatPage()
		{
			InitializeComponent();
			ClientBot.client.MessageReceived += Refresh;
		}

		private SocketTextChannel channel;

		public SocketTextChannel Channel
		{
			get => channel;
			set
			{
				channel = value;
				Refresh();
			}
		}

		private async Task Refresh(SocketMessage m = null)
		{
			if (channel == null)
				return;

			if (m != null && m.Channel != channel)
				return;

			await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
				messagelist.ItemsSource = channel.GetMessagesAsync()
					.ToListAsync().AsTask().Result[1]
					.AsEnumerable().OrderBy(s => s.Timestamp)
			);

			//messagelist.ScrollIntoView(messagelist.Items.Last());
		}

		private void MessageInput_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (channel == null)
				return;

			if (e.Key == VirtualKey.Enter)
			{
				channel.SendMessageAsync(messageInput.Text);
				messageInput.Text = "";
			}
		}
	}
}

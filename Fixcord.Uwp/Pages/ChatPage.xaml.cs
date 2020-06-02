using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
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
				messageList.ItemsSource = channel.GetMessagesAsync()
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

		private void messagelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var isTransient = false;
			FlyoutShowOptions myOption = new FlyoutShowOptions
			{
				ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard
			};
			CommandBarFlyout1.ShowAt(messageInput, myOption);
		}

		private void Copy_Click(object sender, RoutedEventArgs e)
		{
			var message = (RestUserMessage)messageList.SelectedItem;

			var dataPackage = new DataPackage();
			dataPackage.SetText(message.Content);
			Clipboard.SetContent(dataPackage);

			CommandBarFlyout1.Hide();
		}

		private async void Delete_Click(object sender, RoutedEventArgs e)
		{
			var message = messageList.SelectedItems;
			foreach (RestUserMessage item in message)
			{
				try
				{
					await item.DeleteAsync();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
				}
			}
			await Refresh();

			CommandBarFlyout1.Hide();
		}
	}
}

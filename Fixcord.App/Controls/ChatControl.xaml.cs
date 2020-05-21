using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fixcord.App
{
	public partial class ChatControl : UserControl
	{
		public ChatControl()
		{
			InitializeComponent();
			var a = (SocketTextChannel)ClientBot.selectedTextChannel;
			ClientBot.client.MessageReceived += (SocketMessage arg) => RefreshAsync();
		}

		private async Task RefreshAsync()
		{
			SocketTextChannel channel = ClientBot.selectedTextChannel;
			if (channel == null)
			{
				return;
			}

			try
			{
				var a = await channel.GetMessagesAsync().ToListAsync();
				var b = a[1].AsEnumerable().OrderBy(s => s.Timestamp);

				var c = new List<string>();
				foreach (var i in b)
				{
					c.Add($"{i.Author}: {i.Content}");
				}

				messages.ItemsSource = c;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Refreshing chat failed.");
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
			=> RefreshAsync().ConfigureAwait(false);

		private void MessageInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				var b = (IMessageChannel)ClientBot.selectedTextChannel;
				b.SendMessageAsync(messageInput.Text);
				messageInput.Text = null;

				RefreshAsync().ConfigureAwait(false);
			}
		}
	}
}

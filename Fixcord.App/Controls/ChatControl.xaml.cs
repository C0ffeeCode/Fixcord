using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fixcord.App.Controls
{
	public partial class ChatControl : UserControl
	{
		public ChatControl()
		{
			InitializeComponent();
			var a = ClientBot.SelectedTextChannel;
			ClientBot.client!.MessageReceived += (SocketMessage arg) => RefreshAsync();
			ClientBot.SelectedTextChannelChanged += () => RefreshAsync().ConfigureAwait(false);
		}

		private async Task RefreshAsync()
		{
			SocketTextChannel channel = ClientBot.SelectedTextChannel!;
			if (channel == null) return;

			try
			{
				var a = await channel.GetMessagesAsync().ToListAsync();
				var b = a[1].AsEnumerable().OrderBy(s => s.Timestamp);

				var c = new List<ChatItem>();
				foreach (var i in b)
				{
					var d = new ChatItem();
					d.Message = i;
					//d.Width = this.Width;
					c.Add(d);
				}
				Dispatcher.Invoke(() =>
				messages.ItemsSource = c);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Refreshing chat failed. " + e);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
			=> RefreshAsync().ConfigureAwait(false);
	}
}

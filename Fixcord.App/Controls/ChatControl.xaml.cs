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
			var a = ClientBot.SelectedTextChannel;
			ClientBot.client!.MessageReceived += (SocketMessage arg) => RefreshAsync();
			ClientBot.client.MessageReceived += Client_MessageReceived;
			ClientBot.SelectedTextChannelChanged += () => RefreshAsync().ConfigureAwait(false);
		}

		private Task Client_MessageReceived(SocketMessage arg)
		{
			if (arg.Channel == ClientBot.SelectedTextChannel)
				RefreshAsync().ConfigureAwait(false);
			return Task.CompletedTask;
		}

		private async Task RefreshAsync()
		{
			SocketTextChannel channel = ClientBot.SelectedTextChannel!;
			if (channel == null)
				return;

			try
			{
				var a = await channel.GetMessagesAsync().ToListAsync();
				var b = a[1].AsEnumerable().OrderBy(s => s.Timestamp);

				var c = new List<string>();
				foreach (var i in b)
				{
					c.Add($"{i.Author}: {i.Content}");
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

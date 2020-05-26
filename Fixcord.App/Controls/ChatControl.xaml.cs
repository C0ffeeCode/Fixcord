using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Fixcord.App.Controls
{
	public partial class ChatControl : UserControl
	{
		public ChatControl()
		{
			InitializeComponent();
			ClientBot.client!.MessageReceived += (SocketMessage m) => Refresh();
			ClientBot.SelectedTextChannelChanged += () => Refresh();
		}

		private Task Refresh()
		{ // Im sorrry, 🍝
			Dispatcher.Invoke(new Action(() =>
					messagelisttest.ItemsSource =
						(ClientBot.SelectedTextChannel!.GetMessagesAsync()
							.ToListAsync().AsTask().Result)[1]
							.AsEnumerable().OrderBy(s => s.Timestamp)
			), DispatcherPriority.ContextIdle);
			return Task.CompletedTask;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
			=> Refresh();
	}
}

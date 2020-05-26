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
			//messagelisttest.ItemsSource = listabc;
			ClientBot.client!.MessageReceived += (SocketMessage arg) =>
			{
				ClientBot.InvokeTextChannelChange();
				return Task.CompletedTask;
			};
			ClientBot.SelectedTextChannelChanged += () => RefreshAsync();
		}

		//return ClientBot.SelectedTextChannel == null ? (IEnumerable<IMessage>)new List<IMessage>() : ClientBot.SelectedTextChannel!.GetCachedMessages().AsEnumerable();

		private void RefreshAsync()
		{
			// Im sorrry, 🍝
			Dispatcher.Invoke(new Action(() =>
			{
				SocketTextChannel channel = ClientBot.SelectedTextChannel!;
				if (channel == null)
				{
					return;
				}

				List<IMessage> listabc = new List<IMessage>();
				try
				{
					var a = channel.GetMessagesAsync().ToListAsync().Result;
					var b = a[1].AsEnumerable().OrderBy(s => s.Timestamp);

					var x = channel.GetMessagesAsync().ToListAsync().Result;
					//listabc = channel.GetMessagesAsync().ToEnumerable();

					foreach (var i in x[1])
					{
						listabc.Add(i);
						//var d = new ChatItem();
						//d.Message = i;
						////d.Width = this.Width;
						//listabc.Add(d);
					}
					//Dispatcher.Invoke(
					//	DispatcherPriority.DataBind,
					//	new Action(() =>
					messagelisttest.ItemsSource = listabc;
				}
				catch (Exception e)
				{
					Debug.WriteLine("Refreshing chat failed. " + e);
				}
				return;
			}), DispatcherPriority.ContextIdle);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
			=> RefreshAsync();
	}
}

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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Fixcord.Uwp
{
	public sealed partial class ChatControl : UserControl
	{
		public ChatControl()
		{
			this.InitializeComponent();
			ClientBot.client.MessageReceived += (SocketMessage m) => Refresh();
			ClientBot.SelectedTextChannelChanged += () => Refresh();
		}

		private Task Refresh()
		{ // Im sorrry, 🍝
		  //Dispatcher.Invoke(new Action(() =>
		  //		messagelisttest.ItemsSource =
		  //			(ClientBot.SelectedTextChannel!.GetMessagesAsync()
		  //				.ToListAsync().AsTask().Result)[1]
		  //				.AsEnumerable().OrderBy(s => s.Timestamp)
		  //), DispatcherPriority.ContextIdle);
			if (ClientBot.SelectedTextChannel == null)
				return Task.CompletedTask;
			try
			{
				var messages = ClientBot.SelectedTextChannel.GetMessagesAsync();
				messagelisttest.ItemsSource =
					(messages
						.ToListAsync().AsTask().Result)[1]
						.AsEnumerable().OrderBy(s => s.Timestamp);
				return Task.CompletedTask;
			}
			catch (Exception e)
			{

				throw e;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
			=> Refresh();
	}
}

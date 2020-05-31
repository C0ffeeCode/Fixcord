using Discord;
using Discord.WebSocket;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Fixcord.Uwp
{
	public delegate void MyDel();

	public class ClientBot
	{
		public static event MyDel SelectedGuildChanged;
		public static event MyDel SelectedTextChannelChanged;
		public static event MyDel SelectedVoiceChannelChanged;

		public static DiscordSocketClient client;

		static SocketGuild selectedGuild;
		static SocketTextChannel selectedTextChannel;
		static SocketVoiceChannel selectedVoiceChannel;

		public static SocketGuild SelectedGuild
		{
			get => selectedGuild; set
			{
				selectedGuild = value;
				SelectedGuildChanged?.Invoke();
			}
		}
		public static SocketTextChannel SelectedTextChannel
		{
			get => selectedTextChannel;
			set
			{
				selectedTextChannel = value;
				SelectedTextChannelChanged?.Invoke();
			}
		}
		public static SocketVoiceChannel SelectedVoiceChannel
		{
			get => selectedVoiceChannel; set
			{
				selectedVoiceChannel = value;
				SelectedVoiceChannelChanged?.Invoke();
			}
		}

		public static Task InvokeTextChannelChange()
		{
			SelectedTextChannelChanged?.Invoke();
			return Task.CompletedTask;
		}

		public async void Initialize()
		{
			client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Info
			});
			try
			{
				var token = Configuration.Token;
				await client.LoginAsync(TokenType.Bot, token, true);
			}
			catch /*(Exeption e)*/
			{
				Debug.WriteLine("Bot token got denied");
			}
			finally
			{
				await client.StartAsync();
			}

			client.Log += Log;
			client.MessageReceived += SendNotification;
		}

		private Task SendNotification(SocketMessage msg)
		{
			//var toastVisual = $@"<visual>
			//		<binding template='ToastGeneric'>
			//			<text>{msg.Channel.Name}: {msg.Author.Username}</text>
			//			<text>{msg.Content}</text>
			//		</binding>
			//	</visual>";
			//var toastActions = "";
			//var toastXmlString = $@"<toast>
			//		{toastVisual}
			//		{toastActions}
			//	</toast>";

			ToastVisual visual = new ToastVisual()
			{
				BindingGeneric = new ToastBindingGeneric()
				{
					Children =
					{
						new AdaptiveText()
						{
							Text = msg.Channel.Name
						},

						new AdaptiveText()
						{
							Text = msg.Author.Username
						}
					}
				}
			};

			ToastContent toastContent = new ToastContent()
			{
				Visual = visual,

				// Arguments when the user taps body of toast
				//Launch = new QueryString()
				//{
				//	{ "action", "viewConversation" },
				//	{ "conversationId", conversationId.ToString() }
				//}.ToString()
			};

			// And create the toast notification
			var toast = new ToastNotification(toastContent.GetXml());
			toast.ExpiresOnReboot = true;
			toast.ExpirationTime = DateTime.Now.AddSeconds(5);

			//XmlDocument toastXml = new XmlDocument();
			//toastXml.LoadXml(toastXmlString);
			//var toast = new ToastNotification(toastXml);
			//toast.ExpirationTime = DateTimeOffset.Parse(TimeSpan.FromSeconds(10).ToString());
			//toast.ExpiresOnReboot = true;

			ToastNotificationManager.CreateToastNotifier().Show(toast);

			return Task.CompletedTask;
		}

		private Task Log(LogMessage message)
		{
			Debug.WriteLine(message.Message);
			return Task.CompletedTask;
		}
	}
}

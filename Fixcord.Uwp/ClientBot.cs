using Discord;
using Discord.WebSocket;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Fixcord.Uwp
{
	public delegate void MyDel();

	public class ClientBot
	{
		public static DiscordSocketClient client;

		#region fields
		public static event MyDel SelectedGuildChanged;
		public static event MyDel SelectedTextChannelChanged;
		public static event MyDel SelectedVoiceChannelChanged;

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
		#endregion

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
			ToastVisual visual = new ToastVisual()
			{
				BindingGeneric = new ToastBindingGeneric()
				{
					Children = {
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
				Visual = visual
			};

			// And create the toast notification
			var toast = new ToastNotification(toastContent.GetXml())
			{
				ExpiresOnReboot = true,
				ExpirationTime = DateTime.Now.AddSeconds(5)
			};
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

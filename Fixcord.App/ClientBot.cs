using Discord;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Fixcord.App
{
	public delegate void MyDel();

	public class ClientBot
	{
		public static event MyDel? SelectedGuildChanged;
		public static event MyDel? SelectedTextChannelChanged;
		public static event MyDel? SelectedVoiceChannelChanged;

		public static DiscordSocketClient? client;

		static SocketGuild? selectedGuild;
		static SocketTextChannel? selectedTextChannel;
		static SocketVoiceChannel? selectedVoiceChannel;

		public static SocketGuild? SelectedGuild
		{
			get => selectedGuild; set
			{
				selectedGuild = value;
				SelectedGuildChanged?.Invoke();
			}
		}
		public static SocketTextChannel? SelectedTextChannel
		{
			get => selectedTextChannel; set
			{
				selectedTextChannel = value;
				SelectedTextChannelChanged?.Invoke();
			}
		}
		public static SocketVoiceChannel? SelectedVoiceChannel
		{
			get => selectedVoiceChannel; set
			{
				selectedVoiceChannel = value;
				SelectedVoiceChannelChanged?.Invoke();
			}
		}

		public static void InvokeTextChannelChange()
		{
			SelectedTextChannelChanged?.Invoke();
		}

		public async void Initialize(string token)
		{
			client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Info
			});
			try
			{
				await client.LoginAsync(TokenType.Bot, token, true);
			}
			catch
			{
				Debug.WriteLine("Bot token got denied");
			}
			finally
			{
				await client.StartAsync();
			}

			client.Log += Log;
		}

		private Task Log(LogMessage message)
		{
			Debug.WriteLine(message.Message);
			return Task.CompletedTask;
		}
	}
}

using Discord;
using Discord.WebSocket;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Fixcord.App
{
	public class ClientBot
	{
		public static DiscordSocketClient? client;
		public static SocketGuild? selectedGuild;
		public static SocketTextChannel? selectedTextChannel;
		public static SocketVoiceChannel? selectedVoiceChannel;

		public async void Initialize(string token)
		{
			client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Debug
			});
			try
			{
				await client.LoginAsync(TokenType.Bot, token, true).ConfigureAwait(true);
			}
			catch
			{
			}
			finally
			{
				await client.StartAsync().ConfigureAwait(true);
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

using System;
using Windows.Security.Credentials;

namespace Fixcord.Uwp
{
	public static class Configuration
	{
		static readonly PasswordVault vault = new PasswordVault();

		const string username = "token";
		const string appname = "Fixcord";
		const string multiWinModeState = "multiWinModeState";

		public static string Token
		{
			get => vault.Retrieve(appname, username).Password;
			set => vault.Add(new PasswordCredential(
				appname, username, value));
		}

		public static bool multiWinModeEnabled
		{
			get
			{
				try
				{
					return Convert.ToBoolean(vault.Retrieve(appname, multiWinModeState).Password);
				}
				catch (Exception)
				{
					return false;
				}
			}
			set => vault.Add(new PasswordCredential(
					appname, multiWinModeState, value.ToString()));
		}
	}
}

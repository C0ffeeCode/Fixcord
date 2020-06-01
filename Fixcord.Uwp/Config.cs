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
		const string notificationsEnabled = "notificationsEnabled";

		public static string Token
		{
			get
			{
				try
				{
					return vault.Retrieve(appname, username).Password;
				}
				catch (Exception)
				{
					return "";
				}
			}
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

		public static bool NotificationsEnabled
		{
			get
			{
				try
				{
					return Convert.ToBoolean(vault.Retrieve(appname, notificationsEnabled).Password);
				}
				catch (Exception)
				{
					return false;
				}
			}
			set => vault.Add(new PasswordCredential(
					appname, notificationsEnabled, value.ToString()));
		}
	}
}

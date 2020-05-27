using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Fixcord.Uwp
{
	public static class Configuration
	{
		static readonly PasswordVault vault = new PasswordVault();

		const string username = "token";
		const string appname = "Fixcord";

		public static string Token
		{ 
			get => vault.Retrieve(appname, username).Password;
			set => vault.Add(new PasswordCredential(
					appname, username, value));
		}
	}
}

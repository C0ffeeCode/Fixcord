using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Fixcord.App
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			Configuration.Load().ConfigureAwait(true);
			InitializeComponent();
		}
	}
}

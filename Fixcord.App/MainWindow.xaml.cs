using System.Windows;
using System.Windows.Input;

namespace Fixcord.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			Configuration.Load().ConfigureAwait(true);
			var x = new ClientBot();
			x.Initialize(Configuration.Config.Token!);
			InitializeComponent();
		}

		private void tokenInput_KeyDown(object sender, KeyEventArgs e)
		{
			Configuration.Config.Token = tokenInput.Text;
			Configuration.Save();
		}
	}
}

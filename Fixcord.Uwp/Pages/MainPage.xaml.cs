using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Fixcord.Uwp.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		readonly ClientBot x = new ClientBot();

		public MainPage()
		{
			x.Initialize();
			InitializeComponent();
			
			// Disable main-window internal Chat component by collapsing it
			if (Configuration.multiWinModeEnabled)
			{
				abc.ColumnDefinitions[2].Width = new GridLength(0);
				//abc.ColumnDefinitions[0].MaxWidth = double.PositiveInfinity;
			}
		}
	}
}

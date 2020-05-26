using Discord;
using System.Windows.Controls;

namespace Fixcord.App.Controls
{
	/// <summary>
	/// Interaction logic for ChatItem.xaml
	/// </summary>
	public partial class ChatItem : UserControl
	{

		public ChatItem()
		{
			InitializeComponent();
		}

		private IMessage a;

		public IMessage Message
		{
			get => a;
			set
			{
				a = value;
				authorName.Content = a.Author.Username;

				//authorProfilePicture.Source =  a.Author.AvatarId
				messageText.Text = a.Content;
			}
		}
	}
}

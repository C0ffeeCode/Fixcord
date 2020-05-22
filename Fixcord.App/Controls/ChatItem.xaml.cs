using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
				authorName.Text = a.Author.Username;
				//authorProfilePicture.Source =  a.Author.AvatarId
				messageText.Text = a.Content;
			}
		}
	}
}

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace TabletopRolePlayingCharacterManager.Views
{
	public sealed partial class CharacterSheet
	{
		public CharacterSheet ()
		{
			InitializeComponent();
		}

		public void GeneralTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
		{
			var senderPanel = sender as Panel;
			if (senderPanel == null)
			{
				throw new InvalidCastException("sender of general tapped wasn't a panel or panel child type");
				
			}
			var parentPanel = senderPanel.Parent as Panel;
			if (parentPanel == null)
			{
				throw new InvalidCastException("parent of sender wasn't a panel or panel child type");
			}
			foreach (var child in parentPanel.Children)
			{
				if (child != sender)
				{
					child.Visibility = child.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
				}
			}
		}
	}
}
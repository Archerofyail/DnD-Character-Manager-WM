using System;
using TabletopRolePlayingCharacterManager.ViewModel;
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
			//ClassAndRaceInfo.Visibility = ClassAndRaceInfo.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Collapsed;
		}

		private void AbilityScoresHeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			//AbilityScores.Visibility = AbilityScores.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Collapsed;
		}

		private void PhysicalDescriptionHeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			//PhysicalDescription.Visibility = PhysicalDescription.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Collapsed;
		}
	}
}
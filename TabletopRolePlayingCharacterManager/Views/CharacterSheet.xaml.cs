using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace TabletopRolePlayingCharacterManager.Views
{
	public sealed partial class CharacterSheet
	{
		public CharacterSheet()
		{
			InitializeComponent();

		}

		public void GeneralTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
		{
			ClassAndRaceInfo.Visibility = ClassAndRaceInfo.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		private void AbilityScoresHeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			AbilityScores.Visibility = AbilityScores.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		private void PhysicalDescriptionHeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			PhysicalDescription.Visibility = PhysicalDescription.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		private void DeleteCharacterClick(object sender, RoutedEventArgs e)
		{

		}

		private void AddSpellButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			AddSpellButton.Flyout?.Hide();
		}

		private void AddItemButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			AddItemButton.Flyout?.Hide();
		}

		private void AddWeaponButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			AddWeaponButton.Flyout?.Hide();
		}

		private void AddTraitTapped(object sender, TappedRoutedEventArgs e)
		{
			AddTraitButton.Flyout?.Hide();
		}

		private void AddNewLangTapped(object sender, TappedRoutedEventArgs e)
		{
			AddNewLangButton.Flyout?.Hide();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			SaveButton.Command?.Execute(SaveButton);
			base.OnNavigatedFrom(e);
		}


		private void DeleteCharacterTapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}

		private void AddNewProficiencyTapped(object sender, TappedRoutedEventArgs e)
		{
			AddNewProficiencyButton.Flyout?.Hide();
		}
	}
}
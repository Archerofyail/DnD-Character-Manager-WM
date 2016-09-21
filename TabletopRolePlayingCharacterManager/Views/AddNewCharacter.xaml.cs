using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using TabletopRolePlayingCharacterManager.ViewModel;

namespace TabletopRolePlayingCharacterManager.Views
{

	public sealed partial class AddNewCharacter : Page
	{
		public AddNewCharacter()
		{
			this.InitializeComponent();
			this.DataContext = new AddNewCharacterPageViewModel();
			Debug.WriteLine("added data context");
		}

		private void LanguageAddClick(object sender, RoutedEventArgs e)
		{
			
		}

		private void ArmorAddClick(object sender, RoutedEventArgs e)
		{
			
		}

		private void WeaponAddClick(object sender, RoutedEventArgs e)
		{
			
		}


		private void RollAbilityScoresClick(object sender, RoutedEventArgs e)
		{
			((AddNewCharacterPageViewModel) DataContext).RollAbilityScores();
			RolledAbilityScoresList.Visibility = Visibility.Visible;
		}

		private void SelectAbilityToAssign(object sender, RoutedEventArgs e)
		{
			var element = (Button) sender;
			var stackPanel = (StackPanel) element.Parent;
			((TextBox)stackPanel.Children[1]).Text = ((AddNewCharacterPageViewModel) DataContext).RolledAbilityScores[RolledAbilityScoresList.SelectedIndex];
			((AddNewCharacterPageViewModel) DataContext).RolledAbilityScores.RemoveAt(RolledAbilityScoresList.SelectedIndex);
		}

		private void ClearScoresClick(object sender, RoutedEventArgs e)
		{
			StrengthTextBox.Text = "";
			DexterityTextBox.Text = "";
			ConstitutionTextBox.Text = "";
			IntelligenceTextBox.Text = "";
			WisdomTextBox.Text = "";
			CharismaTextBox.Text = "";
		}

		private void SaveButtonClick(object sender, RoutedEventArgs e)
		{
			((AddNewCharacterPageViewModel) DataContext).CreateCharacter();

		}
	}
}
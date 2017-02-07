using System.Diagnostics;
using Windows.UI.Xaml;

namespace TabletopRolePlayingCharacterManager.Views
{

	public sealed partial class AddNewCharacter
	{
		public AddNewCharacter()
		{
			InitializeComponent();
			
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
			
		}

		private void SelectAbilityToAssign(object sender, RoutedEventArgs e)
		{
			
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
			

		}
	}
}
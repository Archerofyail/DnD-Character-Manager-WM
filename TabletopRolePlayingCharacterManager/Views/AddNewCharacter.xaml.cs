using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace TabletopRolePlayingCharacterManager.Views
{

	public sealed partial class AddNewCharacter
	{
		public AddNewCharacter()
		{
			InitializeComponent();
			
			Debug.WriteLine("added data context");
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

		private void ClearScores_OnTapped(object sender, TappedRoutedEventArgs e)
		{
			Debug.WriteLine("Clear Tapped");
		}

		private void FinishCharCreationTapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(CharacterSheet));
		}
	}
}
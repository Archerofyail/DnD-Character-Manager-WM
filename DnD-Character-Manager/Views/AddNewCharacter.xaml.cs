using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using DnD_Character_Manager.ViewModel;

namespace DnD_Character_Manager.Views
{
	
	public sealed partial class AddNewCharacter : Page
	{
		public AddNewCharacter()
		{
			this.InitializeComponent();
			this.DataContext = new AddNewCharacterPageViewModel();
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
	}
}
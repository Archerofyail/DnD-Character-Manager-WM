using System.Diagnostics;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using TabletopRolePlayingCharacterManager.Types;
using TabletopRolePlayingCharacterManager.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TabletopRolePlayingCharacterManager
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
			var resourcenames = GetType().GetTypeInfo().Assembly.GetManifestResourceNames();

			if (resourcenames.Length == 0)
			{
				Debug.WriteLine("resourceNames is empty");
			}
			foreach (var name in resourcenames)
			{
				Debug.WriteLine("Found resource name: " + name);

			}
			LoadCompendium();
			//DataContext = new MainPageViewModel();
			//JsonLoader.resourceLoader = ResourceLoader.GetForViewIndependentUse();


		}


		async void LoadCompendium()
		{
			await CharacterManager.LoadCompendium();
		}

		private void SettingsClicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(SettingsPage));
		}

		private void CharacterList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Frame.Navigate(typeof(CharacterSheet));
		}

		private void AddNewCharacterTapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddNewCharacter));
		}

		private void CompendiumTapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(Compendium));
		}
	}
}

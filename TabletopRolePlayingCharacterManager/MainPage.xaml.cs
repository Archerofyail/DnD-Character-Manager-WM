using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;
using TabletopRolePlayingCharacterManager.Views;


namespace TabletopRolePlayingCharacterManager
{

	public sealed partial class MainPage : Page
	{

		public ListView CharacterList { get; set; }

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
			NavigationCacheMode = NavigationCacheMode.Required;
			CharacterSection.Loaded += (sender, args) =>
			{
				var charList = CharacterSection.FindDescendantByName("CharacterList") as ListView;
				if (charList != null)
				{
					CharacterList = charList;
				}
			};
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

		private void CharacterList_OnItemClick(object sender, ItemClickEventArgs e)
		{
			CharacterManager.CurrentCharacter = CharacterManager.Characters[CharacterList.Items.IndexOf(e.ClickedItem)];
			Frame.Navigate(typeof(CharacterSheet));
		}
	}
}

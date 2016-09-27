using System;
using System.Diagnostics;
using System.Reflection;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using TabletopRolePlayingCharacterManager.Views;
using TabletopRolePlayingCharacterManager.Types;
using TabletopRolePlayingCharacterManager.ViewModel;

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
		
			DataContext = new MainPageViewModel();
			//JsonLoader.resourceLoader = ResourceLoader.GetForViewIndependentUse();
			
		}

		private async void AddButtonClicked(object sender, RoutedEventArgs e)
		{

			await CharacterTemplateDialog.ShowAsync();
		}

		private void CharacterTemplateListClicked(object sender, TappedRoutedEventArgs e)
		{

			var dm = DataContext as MainPageViewModel;
			if (dm != null)
			{
				if (dm.SelectedCharacterTemplate == 0)
				{
					Frame.Navigate(typeof(AddNewCharacter));
				}
				else if (dm.SelectedCharacterTemplate == 1)
				{
					Frame.Navigate(typeof(AddNewGenericCharacter));
				}
				else
				{
					ApplicationData.Current.LocalSettings.Values["TemplateChosen"] = dm.SelectedCharacterTemplate - 2;
					Frame.Navigate(typeof(AddNewGenericCharacter));
				}
			}
		}

		private void SettingsClicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(SettingsPage));
		}
	}
}

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
using System.Threading.Tasks;
using Windows.UI.Core;

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

			//DataContext = new MainPageViewModel();
			//JsonLoader.resourceLoader = ResourceLoader.GetForViewIndependentUse();
			
			
		}

		


		private void SettingsClicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(SettingsPage));
		}

		private void CharacterTemplateList_ItemClick(object sender, ItemClickEventArgs e)
		{

			Frame.Navigate(typeof(CharacterSheet));
		}
	}
}

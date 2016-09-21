using System.Diagnostics;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TabletopRolePlayingCharacterManager.Views;
using TabletopRolePlayingCharacterManager.Types;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TabletopRolePlayingCharacterManager
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
			var resourcenames = GetType().GetTypeInfo().Assembly.GetManifestResourceNames();
			 
			if (resourcenames.Length == 0)
			{
				Debug.WriteLine("resourceNames is empty");
			}
			foreach (var name in resourcenames)
			{
				Debug.WriteLine("Found resource name: " + name);

			}
			DBLoader.CreateData();
			//JsonLoader.resourceLoader = ResourceLoader.GetForViewIndependentUse();
		}

		private void AddButtonClicked(object sender, RoutedEventArgs e)
		{
			
			Frame.Navigate(typeof (AddNewCharacter));
		}
	}
}

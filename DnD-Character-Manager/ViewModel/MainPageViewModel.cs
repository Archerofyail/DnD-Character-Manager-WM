using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml;
using DnD_Character_Manager.Model;

namespace DnD_Character_Manager.ViewModel
{
	class MainPageViewModel
	{
		private ObservableCollection<CharacterModel5E> characterList = new ObservableCollection<CharacterModel5E>();
		public ObservableCollection<CharacterModel5E> CharacterList
		{
			get
			{
				if (characterList.Count == 0)
				{
					LoadCharactersFromFile();
				}
				return characterList;
			}
		}

		public MainPageViewModel()
		{
			
		}

		public async void LoadCharactersFromFile()
		{
			StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
			StorageFolder characterFolder = await StorageFolder.GetFolderFromPathAsync(roamingFolder.Path);
			IReadOnlyList<StorageFile> files = await characterFolder.GetFilesAsync();
			foreach (var charFile in files)
			{
				try
				{
					string charJsonString = await FileIO.ReadTextAsync(charFile);
					
				}
				catch (Exception)
				{
					
					throw;
				}
			}
		}
	}
}

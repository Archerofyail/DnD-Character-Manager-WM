using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	class MainPageViewModel
	{
		private ObservableCollection<Character5E> characterList = new ObservableCollection<Character5E>();
		public ObservableCollection<Character5E> CharacterList
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

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
					//foreach (var character in DBLoader.Characters)
					//{
					//	//characterList.Add(character);
					//}

				}
				return characterList;
			}
		}
		public string CharListEmptyText
		{
			get { return "You don't have any characters created, click the add new character button in the bottom right to get started"; }
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

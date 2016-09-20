﻿using System;
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
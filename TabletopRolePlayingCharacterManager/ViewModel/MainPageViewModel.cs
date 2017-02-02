using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;
using TabletopRolePlayingCharacterManager.Views;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	class MainPageViewModel : ViewModelBase
	{

		private int _selectedCharacterTemplate;

		public int SelectedCharacterTemplate
		{
			get
			{ return _selectedCharacterTemplate; }
			set { _selectedCharacterTemplate = value; RaisePropertyChanged(); }
		}

		private ObservableCollection<string> _characterTemplates = new ObservableCollection<string>();

		public ObservableCollection<string> CharacterTemplates
		{
			get
			{
				if (_characterTemplates.Count != 0) return _characterTemplates;
				_characterTemplates.Add("Fifth Edition Character");
				_characterTemplates.Add("Generic Character");
				foreach (var characterTemplate in DbLoader.CharacterTemplates)
				{
					_characterTemplates.Add(characterTemplate.TemplateName);
				}
				return _characterTemplates;
			}
		}

		private ObservableCollection<Character5E> _characterList = new ObservableCollection<Character5E>();

		public ObservableCollection<Character5E> CharacterList
		{
			get
			{
				if (_characterList.Count == 0)
				{
					foreach (var character in DbLoader.Characters)
					{
						_characterList.Add(character);
					}

				}
				return _characterList;
			}
		}

		public string CharListEmptyText
			=> "You don't have any characters created yet!\nClick the add new character button in the bottom right to get started";


		#region UIControl

		

		#endregion


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

		public void CharacterTemplateListClicked()
		{
			
		}

	}
}

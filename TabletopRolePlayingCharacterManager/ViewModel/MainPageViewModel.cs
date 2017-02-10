using System.Collections.ObjectModel;
using TabletopRolePlayingCharacterManager.Types;
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
				//foreach (var characterTemplate in DbLoader.CharacterTemplates)
				//{
				//	_characterTemplates.Add(characterTemplate.TemplateName);
				//}
				return _characterTemplates;
			}
		}

		private ObservableCollection<CharacterViewModel> _characterList = new ObservableCollection<CharacterViewModel>();

		public ObservableCollection<CharacterViewModel> Characters
		{
			get
			{
				if (_characterList.Count == 0)
				{
					CharacterManager.LoadCompendium();
					foreach (var character in CharacterManager.Characters)
					{
						_characterList.Add(new CharacterViewModel(character));
					}
					
				}
				
				return _characterList;
			}
		}

		private int charListIndex = -1;

		public int CharListIndex
		{
			get { return charListIndex; }
			set { CharacterManager.CurrentCharacter = CharacterManager.Characters[value]; }
		}


		public string CharListEmptyText
			=> "You don't have any characters created yet!\nClick the add new character button in the bottom right to get started";


		#region UIControl

		public bool NoCharacters
		{
			get { return Characters.Count == 0 ? true : false; }
		}

		#endregion
		
		public void CharacterTemplateListClicked()
		{
			
		}

	}
}

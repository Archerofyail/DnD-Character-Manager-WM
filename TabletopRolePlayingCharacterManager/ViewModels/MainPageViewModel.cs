using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	class MainPageViewModel : ViewModelBase
	{


		public MainPageViewModel()
		{
			CharacterManager.CharactersLoadedEventHandler += (sender, args) => { RaisePropertyChanged("Characters");};
		}
		private int selectedCharacterTemplate;

		public int SelectedCharacterTemplate
		{
			get
			{ return selectedCharacterTemplate; }
			set { selectedCharacterTemplate = value; RaisePropertyChanged(); }
		}

		private ObservableCollection<string> characterTemplates = new ObservableCollection<string>();

		public ObservableCollection<string> CharacterTemplates
		{
			get
			{
				if (characterTemplates.Count != 0) return characterTemplates;
				characterTemplates.Add("Fifth Edition Character");
				characterTemplates.Add("Generic Character");
				return characterTemplates;
			}
		}

		private ObservableCollection<CharacterViewModel> characterList = new ObservableCollection<CharacterViewModel>();

		public ObservableCollection<CharacterViewModel> Characters
		{
			get
			{
				if (characterList.Count == 0)
				{
					
					characterList.Clear();
					foreach (var character in CharacterManager.Characters)
					{
						characterList.Add(new CharacterViewModel(character));
					}
					RaisePropertyChanged("NoCharacters");
					
				}
				
				return characterList;
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

		

	}
}

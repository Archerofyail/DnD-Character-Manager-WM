using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	class CharacterViewModel : ViewModelBase
	{
		public CharacterViewModel(Character5E ch)
		{
			character = ch;
		}
		private Character5E character;
		public string Name
		{
			get { return character.Name; }
		}

		public string ClassAndLevel
		{
			get { return "Level " + character.Level + " " + character.Class; }
		}
	}
}

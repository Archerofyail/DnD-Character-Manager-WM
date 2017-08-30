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
		public string Name => character.Name;

		public string ClassAndLevel => "Level " + character.Level + " " + character.Class;
	}
}

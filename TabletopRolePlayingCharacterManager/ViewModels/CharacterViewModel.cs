using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	class CharacterViewModel : ViewModelBase
	{
		public CharacterViewModel(Character5E ch)
		{
			_character = ch;
		}
		private Character5E _character;
		public string Name => _character.Name;

		public string ClassAndLevel => "Level " + _character.Level + " " + _character.Class;
		public string Campaign => "Campaign: " + _character.Campaign;
	}
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	//If the CharacterManager's current character is empty when loading this page, assume a new character was created.
	public class CharacterSheetViewModel : ViewModelBase
	{
		private string charName = "New Character";

		public string CharacterName
		{
			get { return charName; }
			set
			{
				charName = value;
				RaisePropertyChanged();
			}
		}
	}
}

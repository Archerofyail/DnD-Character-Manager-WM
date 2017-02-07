using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class SpellViewModel : ViewModelBase
	{
		private Spell spell;

		public string Name
		{
			get { return spell.Name; }
			set
			{
				spell.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get { return spell.Description; }
			set
			{
				spell.Description = value;
				RaisePropertyChanged();
			}
		}


	}
}

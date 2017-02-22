using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class ProficiencyViewModel : GenericItemViewModel
	{
		private Proficiency proficiency;
		public ProficiencyViewModel()
		{
			proficiency = item as Proficiency;
		}

		public string Name
		{
			get { return proficiency.Name; }
			set
			{
				proficiency.Name = value;
				RaisePropertyChanged();
			}
		}
	}
}

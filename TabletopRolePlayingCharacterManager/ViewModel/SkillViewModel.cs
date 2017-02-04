using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	class SkillViewModel : ViewModelBase
	{
		public string Name { get; set; }
		public string Level { get; set; }
		public bool IsProficient { get; set; }
	}
}

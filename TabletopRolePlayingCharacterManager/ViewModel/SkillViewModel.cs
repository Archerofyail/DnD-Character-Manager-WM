using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class SkillViewModel : ViewModelBase
	{
		public string Name { get; set; }
		public int Level { get; set; }
		public bool IsProficient { get; set; }
		public MainStat MainStat { get; set; }
	}
}

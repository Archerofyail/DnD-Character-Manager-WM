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
		public int Bonus { get; set; }
		public bool IsProficient { get; set; }
		private MainStat mStat;
		public string MainStat
		{
			get
			{
				return mStat.ToString().Substring(0, 3);
			}

			set
			{
				MainStat result = TabletopRolePlayingCharacterManager.MainStat.Strength;
				if (Enum.TryParse(value, out result))
				{
					mStat = result;

				}
			}
		}
	}
}

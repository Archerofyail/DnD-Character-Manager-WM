using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class SkillViewModel : ViewModelBase
	{

		public SkillViewModel(Skill skill)
		{
			this.skill = skill;
		}
		private Skill skill;
		public string Name
		{
			get { return skill.Name; }
			set { skill.Name = value; }
		}
		public int Bonus
		{
			get { return skill.Bonus; }
			set { skill.Bonus = value; }
		}
		public bool IsProficient {
			get { return skill.IsProficient; }
			set { skill.IsProficient = value; }
		}
		
		public string MainStat
		{
			get
			{
				return skill.MainStat.ToString().Substring(0, 3);
			}

			set
			{
				MainStat result = TabletopRolePlayingCharacterManager.MainStat.Strength;
				if (Enum.TryParse(value, out result))
				{
					skill.MainStat = result;

				}
			}
		}
	}
}

using System;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
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
			set
			{
				skill.Name = value;
				RaisePropertyChanged();
			}
		}
		public int Bonus
		{
			get { return skill.Bonus; }
			set
			{
				skill.Bonus = value;
				RaisePropertyChanged();
			}
		}
		public bool IsProficient
		{
			get { return skill.IsProficient; }
			set
			{
				skill.IsProficient = value;
				skill.CalculateBonus();
				RaisePropertyChanged("Bonus");
				RaisePropertyChanged();
			}
		}

		public void RaiseAllPropertiesChanged()
		{
			RaisePropertyChanged("Name");
			RaisePropertyChanged("Bonus");
			RaisePropertyChanged("IsProficient");
		}

		public string MainStat
		{
			get
			{
				return skill.MainStat.ToString().Substring(0, 3);
			}

			set
			{
				MainStatType result;
				if (Enum.TryParse(value, out result))
				{
					skill.MainStat = result;

				}
				RaisePropertyChanged();
			}
		}
	}
}

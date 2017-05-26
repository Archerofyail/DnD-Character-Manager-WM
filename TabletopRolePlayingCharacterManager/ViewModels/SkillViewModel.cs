using System;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class SkillViewModel : GenericItemViewModel
	{

		public SkillViewModel(Skill skill)
		{
			Item = skill;
		}

		public SkillViewModel()
		{

		}

		private Skill skill => Item as Skill;
		public string Name
		{
			get => skill.Name;
			set
			{
				skill.Name = value;
				RaisePropertyChanged();
			}
		}
		public int Bonus
		{
			get => skill.Bonus;
			set
			{
				skill.Bonus = value;
				RaisePropertyChanged();
			}
		}
		public bool IsProficient
		{
			get => skill.IsProficient;
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
			get => skill.MainStat.ToString().Substring(0, 3);

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

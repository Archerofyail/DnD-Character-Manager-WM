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

		private Skill Skill => Item as Skill;
		public string Name
		{
			get => Skill.Name;
			set
			{
				Skill.Name = value;
				RaisePropertyChanged();
			}
		}
		public int Bonus
		{
			get => Skill.Bonus;
			set
			{
				Skill.Bonus = value;
				RaisePropertyChanged();
			}
		}
		public bool IsProficient
		{
			get => Skill.IsProficient;
			set
			{
				Skill.IsProficient = value;
				Skill.CalculateBonus();
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
			get => Skill.MainStat.ToString().Substring(0, 3);

			set
			{
				if (Enum.TryParse(value, out MainStatType result))
				{
					Skill.MainStat = result;

				}
				RaisePropertyChanged();
			}
		}
	}
}

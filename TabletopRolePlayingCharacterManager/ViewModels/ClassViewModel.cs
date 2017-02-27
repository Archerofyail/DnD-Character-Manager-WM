using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	//TODO: Figure out how to update the class and race libraries if the names are changed
	public class ClassViewModel : ViewModelBase
	{

		private ClassBonus classBonus;

		public ClassViewModel(ClassBonus cl)
		{
			classBonus = cl;
		}

		public string Name
		{
			get { return classBonus.ClassName; }
			set
			{
				classBonus.ClassName = value;
				RaisePropertyChanged();
			}
		}

		public string SubclassName
		{
			get { return classBonus.SubclassTitle; }
			set
			{
				classBonus.SubclassTitle = value;
				RaisePropertyChanged();
			}
		}

		public int SubclassLevel
		{
			get { return classBonus.SubclassLevel; }
			set { classBonus.SubclassLevel = value; }
		}

		public ObservableCollection<string> DieTypes
		{
			get
			{
				ObservableCollection<string> types = new ObservableCollection<string>();
				foreach (var dieType in Enum.GetValues(typeof(DieType)))
				{
					types.Add(dieType.ToString());
				}
				return types;
			}
		}

		private int selectedHitDieIndex = -1;

		public int SelectedHitDieIndex
		{
			get { return selectedHitDieIndex; }
			set
			{
				selectedHitDieIndex = value;
				classBonus.HitDie = (DieType) Enum.Parse(typeof(DieType), DieTypes[selectedHitDieIndex]);
			}
		}



		public int StartingHP
		{
			get { return classBonus.StartingHP; }
			set { classBonus.StartingHP = value; }
		}

		public int MinHPPerLevel
		{
			get { return classBonus.MinHPPerLevel; }
			set { classBonus.MinHPPerLevel = value; }
		}

		public int NumberofStartingSkills
		{
			get { return classBonus.NumberOfSkills; }
			set
			{
				classBonus.NumberOfSkills = value;
				if (skillChoice != null)
				{
					skillChoice.TotalBonus = classBonus.NumberOfSkills;
				}
			}
		}

		private ChoiceViewModel<Skill, SkillViewModel> skillChoice;

		public ChoiceViewModel<Skill, SkillViewModel> SkillChoice
		{
			get
			{
				if (skillChoice == null)
				{
					skillChoice = new ChoiceViewModel<Skill, SkillViewModel>(classBonus.Skills, classBonus.NumberOfSkills, true);
				}
				return skillChoice;
			}
		}

		public ObservableCollection<string> MainStatsList { get; set; } = new ObservableCollection<string>(Enum.GetNames(typeof(MainStatType)));

		private int selectedMainSpellcastingStatIndex = -1;
		public int SelectedMainSpellcastingStatIndex
		{
			get { return selectedMainSpellcastingStatIndex; }
			set
			{
				selectedMainSpellcastingStatIndex = value;
				classBonus.SpellcastingStat = (MainStatType) Enum.Parse(typeof(MainStatType), MainStatsList[value]);
			}
		}
		private ObservableCollection<PrimitiveListViewModel<int>> spellSlotIncreasesByLevel = new ObservableCollection<PrimitiveListViewModel<int>>();
		public ObservableCollection<PrimitiveListViewModel<int>> SpellSlotIncreasesByLevel
		{
			get
			{
				if (spellSlotIncreasesByLevel.Count == 0)
				{
					foreach (var level in classBonus.SpellSlotIncreasesByLevel)
					{
						spellSlotIncreasesByLevel.Add(new PrimitiveListViewModel<int>(level));
					}
				}
				return spellSlotIncreasesByLevel;
			}
		}

		public string ClassBonusesString
		{
			get
			{
				var text = "";
				foreach (var levelTrait in classBonus.TraitsByLevel)
				{
					text += "Level " + levelTrait.Key + ":\n";
					foreach (var traitList in levelTrait.Value)
					{
						foreach (var trait in traitList)
						{
							text += trait.Description + "\n";
						}

					}
				}
				return text;
			}
		}
	}
}

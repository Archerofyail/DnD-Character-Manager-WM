using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	//TODO: Figure out how to update the class and race libraries if the names are changed
	public class ClassViewModel : ViewModelBase
	{

		public ObservableCollection<int> Levels => new ObservableCollection<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

		private ClassBonus classBonus;

		public ClassViewModel(ClassBonus cl)
		{
			classBonus = cl;
		}

		public ClassViewModel()
		{
			classBonus = new ClassBonus();
		}

		public string Name
		{
			get => classBonus.ClassName;
			set
			{
				classBonus.ClassName = value;
				RaisePropertyChanged();
			}
		}

		public string SubclassName
		{
			get => classBonus.SubclassTitle;
			set
			{
				classBonus.SubclassTitle = value;
				RaisePropertyChanged();
			}
		}

		public int SubclassLevel
		{
			get => classBonus.SubclassLevel;
			set => classBonus.SubclassLevel = value;
		}

		public ObservableCollection<string> DieTypes
		{
			get
			{
				var types = new ObservableCollection<string>();
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
			get => selectedHitDieIndex;
			set
			{
				selectedHitDieIndex = value;
				classBonus.HitDie = (DieType)Enum.Parse(typeof(DieType), DieTypes[selectedHitDieIndex]);
			}
		}



		public int StartingHP
		{
			get => classBonus.StartingHP;
			set => classBonus.StartingHP = value;
		}

		public int MinHPPerLevel
		{
			get => classBonus.MinHPPerLevel;
			set => classBonus.MinHPPerLevel = value;
		}

		public int NumberofStartingSkills
		{
			get => classBonus.NumberOfSkills;
			set
			{
				classBonus.NumberOfSkills = value;
				if (skillChoice != null)
				{
					skillChoice.SelectionCount = classBonus.NumberOfSkills;
				}
			}
		}

		private PrimitiveListViewModel<string> skillChoice;

		public PrimitiveListViewModel<string> SkillChoice => skillChoice ??
		                                                     (skillChoice = new PrimitiveListViewModel<string>(classBonus.NumberOfSkills, true, classBonus.Skills));

		public ObservableCollection<string> AllSkills
		{
			get
			{
				var allskills = new ObservableCollection<string>();
				foreach (var skill in CharacterManager.AllSkills)
				{
					allskills.Add(skill.Name);
				}
				return allskills;
			}
		}

		public ObservableCollection<string> MainStatsList { get; set; } = new ObservableCollection<string>(Enum.GetNames(typeof(MainStatType)));

		private int selectedMainSpellcastingStatIndex = -1;
		public int SelectedMainSpellcastingStatIndex
		{
			get => selectedMainSpellcastingStatIndex;
			set
			{
				selectedMainSpellcastingStatIndex = value;
				classBonus.SpellcastingStat = (MainStatType)Enum.Parse(typeof(MainStatType), MainStatsList[value]);
			}
		}
		private ObservableCollection<PrimitiveListViewModel<int>> spellSlotsByLevel = new ObservableCollection<PrimitiveListViewModel<int>>();
		public ObservableCollection<PrimitiveListViewModel<int>> SpellSlotsByLevel
		{
			get
			{
				if (spellSlotsByLevel.Count == 0)
				{
					foreach (var level in classBonus.SpellSlotsByLevel)
					{
						spellSlotsByLevel.Add(new PrimitiveListViewModel<int>(level));
					}
				}
				return spellSlotsByLevel;
			}
		}
		private PrimitiveListViewModel<int> spellsKnownByLevel = new PrimitiveListViewModel<int>();
		public PrimitiveListViewModel<int> SpellsKnownByLevel
		{
			get
			{
				if (spellsKnownByLevel.Count == 0)
				{
					spellsKnownByLevel.AddRange(classBonus.SpellsKnownByLevel);
				}
				return spellsKnownByLevel;
			}
		}

		private PrimitiveListViewModel<int> cantripsKnownByLevel = new PrimitiveListViewModel<int>();
		public PrimitiveListViewModel<int> CantripsKnownByLevel
		{
			get
			{
				if (spellsKnownByLevel.Count == 0)
				{
					spellsKnownByLevel.AddRange(classBonus.SpellsKnownByLevel);
				}
				return spellsKnownByLevel;
			}
		}

		public ObservableCollection<string> ProficiencyTypes => new ObservableCollection<string>(Enum.GetNames(typeof(ProficiencyType)));
		public int SelectedNewProfTypeIndex { get; set; } = -1;
		private ObservableCollection<Proficiency> proficiencies = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> Proficiencies
		{
			get
			{
				if (proficiencies.Count == 0)
				{
					foreach (var prof in classBonus.Proficiencies)
					{
						proficiencies.Add(prof);
					}
				}
				return proficiencies;
			}
		}

		public ObservableCollection<Proficiency> WeaponProficiencies
		{
			get
			{
				var profs = 
					new ObservableCollection<Proficiency>(Proficiencies.Where((x) => x.Type == ProficiencyType.Weapon));
				return profs;
			}
		}

		public ObservableCollection<Proficiency> ArmorProficiencies
		{
			get
			{
				var profs =
					new ObservableCollection<Proficiency>(Proficiencies.Where((x) => x.Type == ProficiencyType.Armor));
				return profs;
			}
		}

		public ObservableCollection<Proficiency> LanguageProficiencies
		{
			get
			{
				var profs =
					new ObservableCollection<Proficiency>(Proficiencies.Where((x) => x.Type == ProficiencyType.Language));
				return profs;
			}
		}

		public ObservableCollection<Proficiency> ToolProficiencies
		{
			get
			{
				var profs =
					new ObservableCollection<Proficiency>(Proficiencies.Where((x) => x.Type == ProficiencyType.Tool));
				return profs;
			}
		}

		private ObservableCollection<MainStatType> savingThrowProficiencies = new ObservableCollection<MainStatType>();

		public ObservableCollection<MainStatType> SavingThrowProficiiencies
		{
			get
			{
				if (savingThrowProficiencies.Count == 0)
				{
					foreach (var proficiency in classBonus.SavingThrowProficiencies)
					{
						savingThrowProficiencies.Add(proficiency);
					}
				}
				return savingThrowProficiencies;
			}
		}

		private PrimitiveListViewModel<int> abilityScoreImprovementsByLevel = new PrimitiveListViewModel<int>();

		public PrimitiveListViewModel<int> AbilityScoreImprovementsByLevel
		{
			get
			{
				if (abilityScoreImprovementsByLevel.Count == 0)
				{
					abilityScoreImprovementsByLevel.AddRange(classBonus.AbilityScoreImprovements);
				}
				return abilityScoreImprovementsByLevel;
			}
		}

		private ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> equipmentChoices = new ObservableCollection<ChoiceViewModel<Item, ItemViewModel>>();

		public ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> EquipmentChoices
		{
			get
			{
				if (equipmentChoices.Count == 0)
				{
					foreach (var choice in classBonus.EquipmentChoices)
					{
						equipmentChoices.Add(new ChoiceViewModel<Item, ItemViewModel>(choice));
					}
				}
				return equipmentChoices;
			}
		}

		private PrimitiveListViewModel<int> expertise = new PrimitiveListViewModel<int>();

		public PrimitiveListViewModel<int> Expertise
		{
			get
			{
				if (expertise.Count == 0)
				{
					expertise.AddRange(classBonus.Expertise);
				}
				return expertise;
			}
		}

		private ObservableCollection<SubclassViewModel> subclasses = new ObservableCollection<SubclassViewModel>();

		public ObservableCollection<SubclassViewModel> Subclasses
		{
			get
			{
				if (subclasses.Count == 0)
				{
					foreach (var subClass in classBonus.SubClasses)
					{
						subclasses.Add(new SubclassViewModel(subClass));
					}
				}
				return subclasses;
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

		#region Commands

		public ICommand AddSkillOption => new RelayCommand<string>(AddSkillOptionExec);
		public ICommand AddProficiency => new RelayCommand<string>(AddProficiencyExec);
		#region Functions

		void AddSkillOptionExec(string skillName)
		{
			skillChoice.Add(skillName ?? "");
		}

		void AddProficiencyExec(string prof)
		{
			proficiencies.Add(new Proficiency((ProficiencyType)Enum.Parse(typeof(ProficiencyType), ProficiencyTypes[SelectedNewProfTypeIndex]), prof));
			SelectedNewProfTypeIndex = -1;
			RaisePropertyChanged("SelectedNewProfTypeIndex");
		}

		#endregion
		#endregion
	}
}

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

		private ClassBonus _classBonus;

		public ClassViewModel(ClassBonus cl)
		{
			_classBonus = cl;
		}

		public ClassViewModel()
		{
			_classBonus = new ClassBonus();
		}

		public string Name
		{
			get => _classBonus.ClassName;
			set
			{
				_classBonus.ClassName = value;
				RaisePropertyChanged();
			}
		}

		public string SubclassName
		{
			get => _classBonus.SubclassTitle;
			set
			{
				_classBonus.SubclassTitle = value;
				RaisePropertyChanged();
			}
		}

		public int SubclassLevel
		{
			get => _classBonus.SubclassLevel;
			set => _classBonus.SubclassLevel = value;
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

		private int _selectedHitDieIndex = -1;

		public int SelectedHitDieIndex
		{
			get => _selectedHitDieIndex;
			set
			{
				_selectedHitDieIndex = value;
				_classBonus.HitDie = (DieType)Enum.Parse(typeof(DieType), DieTypes[_selectedHitDieIndex]);
			}
		}



		public int StartingHp
		{
			get => _classBonus.StartingHP;
			set => _classBonus.StartingHP = value;
		}

		public int MinHpPerLevel
		{
			get => _classBonus.MinHPPerLevel;
			set => _classBonus.MinHPPerLevel = value;
		}

		public int NumberofStartingSkills
		{
			get => _classBonus.NumberOfSkills;
			set
			{
				_classBonus.NumberOfSkills = value;
				if (_skillChoice != null)
				{
					_skillChoice.SelectionCount = _classBonus.NumberOfSkills;
				}
			}
		}

		private PrimitiveListViewModel<string> _skillChoice;

		public PrimitiveListViewModel<string> SkillChoice => _skillChoice ??
		                                                     (_skillChoice = new PrimitiveListViewModel<string>(_classBonus.NumberOfSkills, true, _classBonus.Skills));

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

		private int _selectedMainSpellcastingStatIndex = -1;
		public int SelectedMainSpellcastingStatIndex
		{
			get => _selectedMainSpellcastingStatIndex;
			set
			{
				_selectedMainSpellcastingStatIndex = value;
				_classBonus.SpellcastingStat = (MainStatType)Enum.Parse(typeof(MainStatType), MainStatsList[value]);
			}
		}
		private ObservableCollection<PrimitiveListViewModel<int>> _spellSlotsByLevel = new ObservableCollection<PrimitiveListViewModel<int>>();
		public ObservableCollection<PrimitiveListViewModel<int>> SpellSlotsByLevel
		{
			get
			{
				if (_spellSlotsByLevel.Count == 0)
				{
					foreach (var level in _classBonus.SpellSlotsByLevel)
					{
						_spellSlotsByLevel.Add(new PrimitiveListViewModel<int>(level));
					}
				}
				return _spellSlotsByLevel;
			}
		}
		private PrimitiveListViewModel<int> _spellsKnownByLevel = new PrimitiveListViewModel<int>();
		public PrimitiveListViewModel<int> SpellsKnownByLevel
		{
			get
			{
				if (_spellsKnownByLevel.Count == 0)
				{
					_spellsKnownByLevel.AddRange(_classBonus.SpellsKnownByLevel);
				}
				return _spellsKnownByLevel;
			}
		}

		private PrimitiveListViewModel<int> _cantripsKnownByLevel = new PrimitiveListViewModel<int>();
		public PrimitiveListViewModel<int> CantripsKnownByLevel
		{
			get
			{
				if (_spellsKnownByLevel.Count == 0)
				{
					_spellsKnownByLevel.AddRange(_classBonus.SpellsKnownByLevel);
				}
				return _spellsKnownByLevel;
			}
		}

		public ObservableCollection<string> ProficiencyTypes => new ObservableCollection<string>(Enum.GetNames(typeof(ProficiencyType)));
		public int SelectedNewProfTypeIndex { get; set; } = -1;
		private ObservableCollection<Proficiency> _proficiencies = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> Proficiencies
		{
			get
			{
				if (_proficiencies.Count == 0)
				{
					foreach (var prof in _classBonus.Proficiencies)
					{
						_proficiencies.Add(prof);
					}
				}
				return _proficiencies;
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

		private ObservableCollection<MainStatType> _savingThrowProficiencies = new ObservableCollection<MainStatType>();

		public ObservableCollection<MainStatType> SavingThrowProficiiencies
		{
			get
			{
				if (_savingThrowProficiencies.Count == 0)
				{
					foreach (var proficiency in _classBonus.SavingThrowProficiencies)
					{
						_savingThrowProficiencies.Add(proficiency);
					}
				}
				return _savingThrowProficiencies;
			}
		}

		private PrimitiveListViewModel<int> _abilityScoreImprovementsByLevel = new PrimitiveListViewModel<int>();

		public PrimitiveListViewModel<int> AbilityScoreImprovementsByLevel
		{
			get
			{
				if (_abilityScoreImprovementsByLevel.Count == 0)
				{
					_abilityScoreImprovementsByLevel.AddRange(_classBonus.AbilityScoreImprovements);
				}
				return _abilityScoreImprovementsByLevel;
			}
		}

		private ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> _equipmentChoices = new ObservableCollection<ChoiceViewModel<Item, ItemViewModel>>();

		public ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> EquipmentChoices
		{
			get
			{
				if (_equipmentChoices.Count == 0)
				{
					foreach (var choice in _classBonus.EquipmentChoices)
					{
						_equipmentChoices.Add(new ChoiceViewModel<Item, ItemViewModel>(choice));
					}
				}
				return _equipmentChoices;
			}
		}

		private PrimitiveListViewModel<int> _expertise = new PrimitiveListViewModel<int>();

		public PrimitiveListViewModel<int> Expertise
		{
			get
			{
				if (_expertise.Count == 0)
				{
					_expertise.AddRange(_classBonus.Expertise);
				}
				return _expertise;
			}
		}

		private ObservableCollection<SubclassViewModel> _subclasses = new ObservableCollection<SubclassViewModel>();

		public ObservableCollection<SubclassViewModel> Subclasses
		{
			get
			{
				if (_subclasses.Count == 0)
				{
					foreach (var subClass in _classBonus.SubClasses)
					{
						_subclasses.Add(new SubclassViewModel(subClass));
					}
				}
				return _subclasses;
			}
		}

		public string ClassBonusesString
		{
			get
			{
				var text = "";
				foreach (var levelTrait in _classBonus.TraitsByLevel)
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
			_skillChoice.Add(skillName ?? "");
		}

		void AddProficiencyExec(string prof)
		{
			_proficiencies.Add(new Proficiency((ProficiencyType)Enum.Parse(typeof(ProficiencyType), ProficiencyTypes[SelectedNewProfTypeIndex]), prof));
			SelectedNewProfTypeIndex = -1;
			RaisePropertyChanged("SelectedNewProfTypeIndex");
		}

		#endregion
		#endregion
	}
}

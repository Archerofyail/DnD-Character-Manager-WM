using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class RaceViewModel : ViewModelBase
	{
		public RacialBonus racialBonuses { get; }

		public RaceViewModel(RacialBonus raceBonuses)
		{
			racialBonuses = raceBonuses;
		}

		public RaceViewModel()
		{
			racialBonuses = new RacialBonus();
		}

		public string Name
		{
			get { return racialBonuses.Race; }
			set
			{
				racialBonuses.Race = value;
				RaisePropertyChanged();
			}
		}

		#region Lists
		
		public ObservableCollection<ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>> AbilityScoreBonuses
		{
			get
			{
				var bonuses = new ObservableCollection<ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>>();
				foreach (var statBonuses in racialBonuses.StatBonuses)
				{
					bonuses.Add(new ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>(statBonuses));
				}
				return bonuses;
			}
		}

		#region Proficiencies

		private ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> weaponProficiencies = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> WeaponProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					
					foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Weapon))
					{
						weaponProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
					}
				}
				return weaponProficiencies;
			}
		}
		private ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> armorProficiencies = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> ArmorProficiencies
		{
			get
			{
				if (armorProficiencies.Count == 0)
				{
					
					foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Armor))
					{
						armorProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
					}
				}
				return armorProficiencies;
			}
		}

		private ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> languageProficiencies = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> LanguageProficiencies
		{
			get
			{
				if (languageProficiencies.Count == 0)
				{

					
					foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Language))
					{
						languageProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
					}
				}
				return languageProficiencies;
			}
		}

		private ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> toolProficiencies = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> ToolProficiencies
		{
			get
			{
				if (toolProficiencies.Count == 0)
				{
					foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Tool))
					{
						toolProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
					}
				}
				return toolProficiencies;
			}
		}

		#endregion

		private ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> traits = new ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>>();

		public ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> Traits
		{
			get
			{
				if (traits.Count == 0)
				{
					foreach (var traitList in racialBonuses.Traits)
					{
						traits.Add(new ChoiceViewModel<Trait, TraitViewModel>(traitList));
					}
				}
				return traits;
			}
		}

		#endregion
		#region VisualOnly

		public string StatBonuses
		{
			get
			{
				var text = "";
				foreach (var bonusList in racialBonuses.StatBonuses)
				{
					foreach (var bonus in bonusList)
					{
						text += "+" + bonus.Bonus + bonus.Stat + ", ";
					}

				}
				text = text.Substring(0, text.Length - 2);
				return text;
			}
		}

		public string Speed
		{
			get { return racialBonuses.SpeedBonus.ToString(); }
			set
			{
				if (int.TryParse(value, out int result))
				{
					if (result % 5 == 0)
					{
						racialBonuses.SpeedBonus = result;
						RaisePropertyChanged();
					}
				}
			}
		}

		#endregion

		#region Commands

		public ICommand AddTrait => new RelayCommand(AddTraitExecute);
		public ICommand AddProficiency => new RelayCommand<ProficiencyType>(AddProficiencyExec);
		public ICommand AddAbilityScoreBonus => new RelayCommand(AddAbilityScoreBonusExec);

		void AddAbilityScoreBonusExec()
		{
			AbilityScoreBonuses.Add(new ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>());

		}

		void AddTraitExecute()
		{
			Traits.Add(new ChoiceViewModel<Trait, TraitViewModel>());
		}

		void AddProficiencyExec(ProficiencyType type)
		{
			switch (type)
			{
				case ProficiencyType.Weapon:
				{
					weaponProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Weapon));
					RaisePropertyChanged("WeaponProficiencies");
				}
				break;
				case ProficiencyType.Armor:
				{
					armorProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Armor));
					RaisePropertyChanged("ArmorProficiencies");
				}
				break;
				case ProficiencyType.Tool:
				{
					toolProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Tool));
					RaisePropertyChanged("ToolProficiencies");
				}
				break;
				case ProficiencyType.Language:
				{
					languageProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Language));
					RaisePropertyChanged("LanguageProficiencies");
				}
				break;
			}
		}


		#endregion

	}
}

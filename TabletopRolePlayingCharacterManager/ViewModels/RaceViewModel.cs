using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class RaceViewModel : ViewModelBase
	{
		public RacialBonus RacialBonuses { get; }

		public RaceViewModel(RacialBonus raceBonuses)
		{
			RacialBonuses = raceBonuses;
		}

		public RaceViewModel()
		{
			RacialBonuses = new RacialBonus();
		}

		public string Name
		{
			get => RacialBonuses.Race;
			set
			{
				RacialBonuses.Race = value;
				RaisePropertyChanged();
			}
		}

		#region Lists
		
		public ObservableCollection<ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>> AbilityScoreBonuses
		{
			get
			{
				var bonuses = new ObservableCollection<ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>>();
				foreach (var statBonuses in RacialBonuses.StatBonuses)
				{
					bonuses.Add(new ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>(statBonuses));
				}
				return bonuses;
			}
		}

		#region Proficiencies

		private ObservableCollection<ProficiencyChoiceViewModel> _weaponProficiencies = new ObservableCollection<ProficiencyChoiceViewModel>();
		public ObservableCollection<ProficiencyChoiceViewModel> WeaponProficiencies
		{
			get
			{
				if (_weaponProficiencies.Count == 0)
				{
					
					foreach (var profChoice in RacialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Weapon))
					{
						_weaponProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Weapon, profChoice));
					}
				}
				return _weaponProficiencies;
			}
		}
		private ObservableCollection<ProficiencyChoiceViewModel> _armorProficiencies = new ObservableCollection<ProficiencyChoiceViewModel>();
		public ObservableCollection<ProficiencyChoiceViewModel> ArmorProficiencies
		{
			get
			{
				if (_armorProficiencies.Count == 0)
				{
					
					foreach (var profChoice in RacialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Armor))
					{
						_armorProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Armor, profChoice));
					}
				}
				return _armorProficiencies;
			}
		}

		private ObservableCollection<ProficiencyChoiceViewModel> _languageProficiencies = new ObservableCollection<ProficiencyChoiceViewModel>();
		public ObservableCollection<ProficiencyChoiceViewModel> LanguageProficiencies
		{
			get
			{
				if (_languageProficiencies.Count == 0)
				{

					
					foreach (var profChoice in RacialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Language))
					{
						_languageProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Language, profChoice));
					}
				}
				return _languageProficiencies;
			}
		}

		private ObservableCollection<ProficiencyChoiceViewModel> _toolProficiencies = new ObservableCollection<ProficiencyChoiceViewModel>();
		public ObservableCollection<ProficiencyChoiceViewModel> ToolProficiencies
		{
			get
			{
				if (_toolProficiencies.Count == 0)
				{
					foreach (var profChoice in RacialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Tool))
					{
						_toolProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Tool, profChoice));
					}
				}
				return _toolProficiencies;
			}
		}

		#endregion

		private ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> _traits = new ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>>();

		public ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> Traits
		{
			get
			{
				if (_traits.Count == 0)
				{
					foreach (var traitList in RacialBonuses.Traits)
					{
						_traits.Add(new ChoiceViewModel<Trait, TraitViewModel>(traitList));
					}
				}
				return _traits;
			}
		}

		#endregion
		#region VisualOnly

		public string StatBonuses
		{
			get
			{
				var text = "";
				foreach (var bonusList in RacialBonuses.StatBonuses)
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
			get => RacialBonuses.SpeedBonus.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					if (result % 5 == 0)
					{
						RacialBonuses.SpeedBonus = result;
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
					_weaponProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Weapon));
					RaisePropertyChanged("WeaponProficiencies");
				}
				break;
				case ProficiencyType.Armor:
				{
					_armorProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Armor));
					RaisePropertyChanged("ArmorProficiencies");
				}
				break;
				case ProficiencyType.Tool:
				{
					_toolProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Tool));
					RaisePropertyChanged("ToolProficiencies");
				}
				break;
				case ProficiencyType.Language:
				{
					_languageProficiencies.Add(new ProficiencyChoiceViewModel(ProficiencyType.Language));
					RaisePropertyChanged("LanguageProficiencies");
				}
				break;
			}
		}


		#endregion

	}
}

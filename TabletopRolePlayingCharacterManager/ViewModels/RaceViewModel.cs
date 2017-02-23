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
		private RacialBonus racialBonuses;

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

		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> WeaponProficiencies
		{
			get
			{
				var profs = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
				foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Weapon))
				{
					profs.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
				}
				return profs;
			}
		}

		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> ArmorProficiencies
		{
			get
			{
				var profs = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
				foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Armor))
				{
					profs.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
				}
				return profs;
			}
		}

		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> LanguageProficiencies
		{
			get
			{
				var profs = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
				foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Language))
				{
					profs.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
				}
				return profs;
			}
		}


		public ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>> ToolProficiencies
		{
			get
			{
				var profs = new ObservableCollection<ChoiceViewModel<Proficiency, ProficiencyViewModel>>();
				foreach (var profChoice in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Tool))
				{
					profs.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(profChoice));
				}
				return profs;
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
					WeaponProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>(new List<Proficiency>()));
					RaisePropertyChanged("WeaponProficiencies");
				}
				break;
				case ProficiencyType.Armor:
				{
					ArmorProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>());
					RaisePropertyChanged("ArmorProficiencies");
				}
				break;
				case ProficiencyType.Tool:
				{
					ToolProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>());
					RaisePropertyChanged("ToolProficiencies");
				}
				break;
				case ProficiencyType.Language:
				{
					LanguageProficiencies.Add(new ChoiceViewModel<Proficiency, ProficiencyViewModel>());
					RaisePropertyChanged("LanguageProficiencies");
				}
				break;
			}
		}


		#endregion

	}
}

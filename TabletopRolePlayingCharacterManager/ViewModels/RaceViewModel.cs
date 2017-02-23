using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

						text += "+" + bonus.Item2 + bonus.Item1 + ", ";


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

		public ICommand AddTrait => new RelayCommand(AddTraitExecute );
		public ICommand AddAbilityScoreBonus => new RelayCommand(AddAbilityScoreBonusExec);

		void AddAbilityScoreBonusExec()
		{
			
		}

		void AddTraitExecute()
		{
			
		}
		

		#endregion

	}
}

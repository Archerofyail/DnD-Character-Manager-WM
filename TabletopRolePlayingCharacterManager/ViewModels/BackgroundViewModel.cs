using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class BackgroundViewModel : ViewModelBase
	{
		public Background Background { get; private set; } = new Background();
		public string Name
		{
			get => Background.Name;
			set
			{
				Background.Name = value;
				RaisePropertyChanged();
			}
		}

		public string BackgroundFeatureName
		{
			get => Background.BackgroundFeatureName;
			set
			{
				Background.BackgroundFeatureName = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<string> _skills = new ObservableCollection<string>();
		public ObservableCollection<string> Skills
		{
			get
			{
				if (_skills.Count == 0)
				{
					foreach (var skill in Background.Skills)
					{
						_skills.Add(skill);
					}
				}

				return _skills;
			}
		}
		private ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> _equipmentChoices = new ObservableCollection<ChoiceViewModel<Item, ItemViewModel>>();

		public ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> EquipmentChoices
		{
			get
			{
				if (_equipmentChoices.Count == 0)
				{
					foreach (var equipmentChoice in Background.Equipment)
					{
						_equipmentChoices.Add(new ChoiceViewModel<Item, ItemViewModel>(equipmentChoice));
					}
				}

				return _equipmentChoices;
			}
		}

		private ObservableCollection<ProficiencyChoiceViewModel> _proficiencies = new ObservableCollection<ProficiencyChoiceViewModel>();

		public ObservableCollection<ProficiencyChoiceViewModel> Proficiencies
		{
			get
			{
				if (_proficiencies.Count == 0)
				{
					foreach (var proficiencyChoice in Background.Proficiencies)
					{
						_proficiencies.Add(new ProficiencyChoiceViewModel(proficiencyChoice[0].Type, proficiencyChoice));
					}
				}

				return _proficiencies;
			}
		}

		private ObservableCollection<string> _backgroundFeature = new ObservableCollection<string>();

		public ObservableCollection<string> BackgroundFeature
		{
			get
			{
				if (_backgroundFeature.Count == 0)
				{
					foreach (var feature in Background.BackgroundFeatures)
					{
						_backgroundFeature.Add(feature);
					}
				}

				return _backgroundFeature;
			}
		}

		private ObservableCollection<string> _ideals = new ObservableCollection<string>();

		public ObservableCollection<string> Ideals
		{
			get
			{
				if (_ideals.Count == 0)
				{
					foreach (var feature in Background.Ideals)
					{
						_ideals.Add(feature);
					}
				}

				return _ideals;
			}
		}

		private ObservableCollection<string> _bonds = new ObservableCollection<string>();

		public ObservableCollection<string> Bonds
		{
			get
			{
				if (_bonds.Count == 0)
				{
					foreach (var feature in Background.Bonds)
					{
						_bonds.Add(feature);
					}
				}

				return _bonds;
			}
		}

		private ObservableCollection<string> _flaws = new ObservableCollection<string>();

		public ObservableCollection<string> Flaws
		{
			get
			{
				if (_flaws.Count == 0)
				{
					foreach (var feature in Background.Flaws)
					{
						_flaws.Add(feature);
					}
				}

				return _flaws;
			}
		}

		private ObservableCollection<Trait> _traits = new ObservableCollection<Trait>();

		public ObservableCollection<Trait> Traits
		{
			get
			{
				if (_traits.Count == 0)
				{
					foreach (var trait in Background.Traits)
					{
						_traits.Add(trait);
					}
				}
				return _traits;
			}
		}
	}
}

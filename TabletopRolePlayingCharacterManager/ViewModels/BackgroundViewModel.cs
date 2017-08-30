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
		public Background background { get; private set; } = new Background();
		public string Name
		{
			get => background.Name;
			set
			{
				background.Name = value;
				RaisePropertyChanged();
			}
		}

		public string BackgroundFeatureName
		{
			get => background.BackgroundFeatureName;
			set
			{
				background.BackgroundFeatureName = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<string> skills = new ObservableCollection<string>();
		public ObservableCollection<string> Skills
		{
			get
			{
				if (skills.Count == 0)
				{
					foreach (var skill in background.Skills)
					{
						skills.Add(skill);
					}
				}

				return skills;
			}
		}
		private ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> equipmentChoices = new ObservableCollection<ChoiceViewModel<Item, ItemViewModel>>();

		public ObservableCollection<ChoiceViewModel<Item, ItemViewModel>> EquipmentChoices
		{
			get
			{
				if (equipmentChoices.Count == 0)
				{
					foreach (var equipmentChoice in background.Equipment)
					{
						equipmentChoices.Add(new ChoiceViewModel<Item, ItemViewModel>(equipmentChoice));
					}
				}

				return equipmentChoices;
			}
		}

		private ObservableCollection<ProficiencyChoiceViewModel> proficiencies = new ObservableCollection<ProficiencyChoiceViewModel>();

		public ObservableCollection<ProficiencyChoiceViewModel> Proficiencies
		{
			get
			{
				if (proficiencies.Count == 0)
				{
					foreach (var proficiencyChoice in background.Proficiencies)
					{
						proficiencies.Add(new ProficiencyChoiceViewModel(proficiencyChoice[0].Type, proficiencyChoice));
					}
				}

				return proficiencies;
			}
		}

		private ObservableCollection<string> backgroundFeature = new ObservableCollection<string>();

		public ObservableCollection<string> BackgroundFeature
		{
			get
			{
				if (backgroundFeature.Count == 0)
				{
					foreach (var feature in background.BackgroundFeatures)
					{
						backgroundFeature.Add(feature);
					}
				}

				return backgroundFeature;
			}
		}

		private ObservableCollection<string> ideals = new ObservableCollection<string>();

		public ObservableCollection<string> Ideals
		{
			get
			{
				if (ideals.Count == 0)
				{
					foreach (var feature in background.Ideals)
					{
						ideals.Add(feature);
					}
				}

				return ideals;
			}
		}

		private ObservableCollection<string> bonds = new ObservableCollection<string>();

		public ObservableCollection<string> Bonds
		{
			get
			{
				if (bonds.Count == 0)
				{
					foreach (var feature in background.Bonds)
					{
						bonds.Add(feature);
					}
				}

				return bonds;
			}
		}

		private ObservableCollection<string> flaws = new ObservableCollection<string>();

		public ObservableCollection<string> Flaws
		{
			get
			{
				if (flaws.Count == 0)
				{
					foreach (var feature in background.Flaws)
					{
						flaws.Add(feature);
					}
				}

				return flaws;
			}
		}

		private ObservableCollection<Trait> traits = new ObservableCollection<Trait>();

		public ObservableCollection<Trait> Traits
		{
			get
			{
				if (traits.Count == 0)
				{
					foreach (var trait in background.Traits)
					{
						traits.Add(trait);
					}
				}
				return traits;
			}
		}
	}
}

using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class CompendiumViewModel : ViewModelBase
	{
		private ObservableCollection<RaceViewModel> races = new ObservableCollection<RaceViewModel>();

		public ObservableCollection<RaceViewModel> Races
		{
			get
			{
				if (races.Count == 0)
				{
					foreach (var race in CharacterManager.RacialBonuses)
					{
						races.Add(new RaceViewModel(race));
					}
				}
				
				return races;
			}
		}

		private ObservableCollection<ClassViewModel> classes = new ObservableCollection<ClassViewModel>();

		public ObservableCollection<ClassViewModel> Classes
		{
			get
			{
				if (classes.Count == 0)
				{
					foreach (var cl in CharacterManager.ClassBonuses)
					{
						classes.Add(new ClassViewModel(cl));
					}
				}
				return classes;
			}
		}

		private ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();

		public ObservableCollection<ItemViewModel> Items
		{
			get
			{
				if (items.Count == 0)
				{
					foreach (var item in CharacterManager.AllItems)
					{
						items.Add(new ItemViewModel(item));
					}
				}
				return items;
			}
		}

		private ObservableCollection<WeaponViewModel> weapons = new ObservableCollection<WeaponViewModel>();

		public ObservableCollection<WeaponViewModel> Weapons
		{
			get
			{
				if (weapons.Count == 0)
				{
					foreach (var item in CharacterManager.AllWeapons)
					{
						weapons.Add(new WeaponViewModel(item));
					}
				}
				return weapons;
			}
		}

		private ObservableCollection<SpellViewModel> spells = new ObservableCollection<SpellViewModel>();

		public ObservableCollection<SpellViewModel> Spells
		{
			get
			{
				if (spells.Count == 0)
				{
					foreach (var item in CharacterManager.AllSpells)
					{
						spells.Add(new SpellViewModel(item));
					}
				}
				return spells;
			}
		}
	}
}

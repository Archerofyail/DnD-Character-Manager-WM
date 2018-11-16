using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class CompendiumViewModel : ViewModelBase
	{


		private ObservableCollection<RaceViewModel> _races = new ObservableCollection<RaceViewModel>();

		public ObservableCollection<RaceViewModel> Races
		{
			get
			{
				if (_races.Count == 0)
				{
					foreach (var race in CharacterManager.RacialBonuses)
					{
						_races.Add(new RaceViewModel(race));
					}
				}

				return _races;
			}
		}

		private ObservableCollection<ClassViewModel> _classes = new ObservableCollection<ClassViewModel>();

		public ObservableCollection<ClassViewModel> Classes
		{
			get
			{
				if (_classes.Count == 0)
				{
					foreach (var cl in CharacterManager.ClassBonuses)
					{
						_classes.Add(new ClassViewModel(cl));
					}
				}
				return _classes;
			}
		}

		private ObservableCollection<BackgroundViewModel> _background = new ObservableCollection<BackgroundViewModel>();

		private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();

		public ObservableCollection<ItemViewModel> Items
		{
			get
			{
				if (_items.Count == 0)
				{
					foreach (var item in CharacterManager.AllItems)
					{
						_items.Add(new ItemViewModel(item));
					}
				}
				return _items;
			}
		}

		private ObservableCollection<WeaponViewModel> _weapons = new ObservableCollection<WeaponViewModel>();

		public ObservableCollection<WeaponViewModel> Weapons
		{
			get
			{
				if (_weapons.Count == 0)
				{
					foreach (var item in CharacterManager.AllWeapons)
					{
						_weapons.Add(new WeaponViewModel(item, RemoveWeaponRelay, null));
					}
				}
				return _weapons;
			}
		}

		private ObservableCollection<SpellViewModel> _spells = new ObservableCollection<SpellViewModel>();

		public ObservableCollection<SpellViewModel> Spells
		{
			get
			{
				if (_spells.Count == 0)
				{
					foreach (var item in CharacterManager.AllSpells)
					{
						_spells.Add(new SpellViewModel(item));
					}
				}
				return _spells;
			}
		}

		public ICommand AddNewRace => new RelayCommand(AddNewRaceExec);

		void AddNewRaceExec()
		{
			var newRace = new RacialBonus();
			CharacterManager.RacialBonuses.Add(newRace);
			Races.Add(new RaceViewModel(newRace));
			RaisePropertyChanged("Races");
		}

		public ICommand AddNewClass => new RelayCommand(AddNewClassExec);

		void AddNewClassExec()
		{
			var newClass = new ClassBonus();
			CharacterManager.ClassBonuses.Add(newClass);
			Classes.Add(new ClassViewModel(newClass));
			RaisePropertyChanged("Classes");
		}

		private RelayCommand<WeaponViewModel> RemoveWeaponRelay => new RelayCommand<WeaponViewModel>(RemoveWeaponExec);

		void RemoveWeaponExec(WeaponViewModel weapon)
		{
			Weapons.Remove(weapon);

			RaisePropertyChanged("Weapons");
			
		}
	}
}

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

		private ObservableCollection<BackgroundViewModel> background = new ObservableCollection<BackgroundViewModel>();

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
						weapons.Add(new WeaponViewModel(item, removeWeaponRelay));
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

		private RelayCommand<WeaponViewModel> removeWeaponRelay => new RelayCommand<WeaponViewModel>(RemoveWeaponExec);

		async void RemoveWeaponExec(WeaponViewModel weapon)
		{
			Weapons.Remove(weapon);

			RaisePropertyChanged("Weapons");
			
		}
	}
}

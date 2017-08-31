using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class WeaponViewModel : ViewModelBase
	{
		public Weapon weapon { get; }
		public static ObservableCollection<MainStatType> MainStatTypes = new ObservableCollection<MainStatType>() { MainStatType.Strength, MainStatType.Dexterity };
		public static  ObservableCollection<WeaponRangeType> WeaponRanges = new ObservableCollection<WeaponRangeType>() {WeaponRangeType.Melee, WeaponRangeType.Ranged};
		private RelayCommand<WeaponViewModel> Remove;
		public WeaponViewModel(Weapon weap, RelayCommand<WeaponViewModel> removeCommand)
		{
			weapon = weap;
			selectedMainStat = MainStatTypes.IndexOf(weap.MainStat);
			SelectedWeaponType = WeaponRanges.IndexOf(weap.WeaponRangeType);
			Remove = removeCommand;
		}
		public string Name
		{
			get => weapon.Name;
			set
			{
				weapon.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Damage
		{
			get => weapon.Damage.ToString();
			set
			{
				var matches = Regex.Match(value, @"(\d)([dD]\d{1,3})");
				Debug.WriteLine("Matches: " + matches.Value);
				for (var i = 0; i < matches.Groups.Count; i++)
				{
					Debug.WriteLine("match " + i + " is " + matches.Groups[i].Value);
				}
				if (matches.Success && matches.Groups.Count >= 3)
				{
					var dieType = DieType.D4;
					if (int.TryParse(matches.Groups[1].Value, out int numDice))
					{
						if (Enum.TryParse(matches.Groups[2].Value.ToUpper(), out dieType))
						{
							weapon.Damage.Dice.Clear();
							weapon.Damage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<MainStatType> MainStat => MainStatTypes;

		private int selectedMainStat = -1;

		public int SelectedMainStat
		{
			get { return selectedMainStat; }
			set
			{
				if (value < MainStatTypes.Count && MainStatTypes.Count >= 0)
				{
					selectedMainStat = value;
					weapon.MainStat = MainStatTypes[selectedMainStat];
					RaisePropertyChanged();
				}
			}
		}

		public string AttackBonus
		{
			get => weapon.AttackBonus.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					weapon.AttackBonus = result;
					RaisePropertyChanged();
				}
			}
		}

		public int SelectedWeaponType { get; set; } = -1;

		public ObservableCollection<WeaponRangeType> WeaponTypes => WeaponRanges;


		public string Description
		{
			get => weapon.Description;
			set
			{
				weapon.Description = value; 
				RaisePropertyChanged();
			}
		}

		public ICommand RemoveWeapon => Remove;
	}
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class WeaponViewModel : ViewModelBase
	{
		private Weapon weapon;
		private static ObservableCollection<MainStatType> _mainStat = new ObservableCollection<MainStatType>() { MainStatType.Strength, MainStatType.Dexterity };
		private static  ObservableCollection<WeaponRangeType> _weaponTypes = new ObservableCollection<WeaponRangeType>() {WeaponRangeType.Melee, WeaponRangeType.Ranged};
		public WeaponViewModel(Weapon weap)
		{
			weapon = weap;

		}
		public string Name
		{
			get { return weapon.Name; }
			set
			{
				weapon.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Damage
		{
			get { return weapon.Damage.ToString(); }
			set
			{
				var matches = Regex.Match(value, @"(\d)(d\d{1,2})");
				Debug.WriteLine("Matches: " + matches.Value);
				for (int i = 0; i < matches.Groups.Count; i++)
				{
					Debug.WriteLine("match " + i + " is " + matches.Groups[i].Value);
				}
				if (matches.Success && matches.Groups.Count == 2)
				{
					DieType dieType = DieType.D4;
					int numDice;
					if (int.TryParse(matches.Groups[0].Value, out numDice))
					{
						if (Enum.TryParse(matches.Groups[1].Value, out dieType))
						{
							weapon.Damage.Dice.Clear();
							weapon.Damage.Dice.Add(dieType, numDice);
						}
					}
				}
			}
		}

		public ObservableCollection<MainStatType> MainStat => _mainStat;

		private int selectedMainStat = -1;

		public int SelectedMainStat
		{
			get { return selectedMainStat; }
			set
			{
				if (value < _mainStat.Count && _mainStat.Count >= 0)
				{
					selectedMainStat = value;
					weapon.MainStat = _mainStat[selectedMainStat];
					RaisePropertyChanged();
				}
			}
		}

		public string AttackBonus
		{
			get { return weapon.AttackBonus.ToString(); }
			set
			{
				int result;
				if (int.TryParse(value, out result))
				{
					weapon.AttackBonus = result;
					RaisePropertyChanged();
				}
			}
		}

		private int selectedWeaponType = -1;

		public int SelectedWeaponType
		{
			get { return selectedWeaponType; }
			set { selectedWeaponType = value; }
		}
		public ObservableCollection<WeaponRangeType> WeaponTypes => _weaponTypes;


		public string Description
		{
			get { return weapon.Description; }
			set
			{
				weapon.Description = value; 
				RaisePropertyChanged();
			}

		}

	}
}

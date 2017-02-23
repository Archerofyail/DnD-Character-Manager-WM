using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class WeaponViewModel : ViewModelBase
	{
		private Weapon weapon;
		private static ObservableCollection<MainStatType> mainStat = new ObservableCollection<MainStatType>() { MainStatType.Strength, MainStatType.Dexterity };
		private static  ObservableCollection<WeaponRangeType> weaponTypes = new ObservableCollection<WeaponRangeType>() {TabletopRolePlayingCharacterManager.WeaponRangeType.Melee, TabletopRolePlayingCharacterManager.WeaponRangeType.Ranged};
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
					DieType DieType = DieType.D4;
					if (int.TryParse(matches.Groups[0].Value, out int numDice))
					{
						if (Enum.TryParse(matches.Groups[1].Value, out DieType))
						{
							weapon.Damage.Dice.Clear();
							weapon.Damage.Dice.Add(DieType, numDice);
						}
					}
				}
			}
		}

		public ObservableCollection<MainStatType> MainStat
		{
			get { return mainStat; }
		}

		private int selectedMainStat = -1;

		public int SelectedMainStat
		{
			get { return selectedMainStat; }
			set
			{
				if (value < mainStat.Count && mainStat.Count >= 0)
				{
					selectedMainStat = value;
					weapon.MainStat = mainStat[selectedMainStat];
					RaisePropertyChanged();
				}
			}
		}

		public string AttackBonus
		{
			get { return weapon.AttackBonus.ToString(); }
			set
			{
				if (int.TryParse(value, out int result))
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
		public ObservableCollection<WeaponRangeType> WeaponTypes
		{
			get { return weaponTypes; }
		}

		

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

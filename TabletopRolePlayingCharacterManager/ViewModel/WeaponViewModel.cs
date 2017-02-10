using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class WeaponViewModel : ViewModelBase
	{
		private Weapon weapon;

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
					var numDice = 0;
					DieType DieType = DieType.D4;
					if (int.TryParse(matches.Groups[0].Value, out numDice))
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

		public string WeaponType
		{
			get { return weapon.WeaponType.ToString(); }
			set
			{
				WeaponType type = TabletopRolePlayingCharacterManager.WeaponType.Melee;
				if (Enum.TryParse(value, out type))
				{
					weapon.WeaponType = type;

				}
				RaisePropertyChanged();
			}
		}
	}
}

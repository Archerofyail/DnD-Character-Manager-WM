using System;
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
			get { return weapon.Damage; }
			set
			{
				weapon.Damage = value;
				RaisePropertyChanged();
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

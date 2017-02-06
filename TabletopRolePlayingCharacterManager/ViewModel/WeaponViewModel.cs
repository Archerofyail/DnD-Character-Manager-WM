using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	class WeaponViewModel : ViewModelBase
	{
		private Weapon weapon;
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

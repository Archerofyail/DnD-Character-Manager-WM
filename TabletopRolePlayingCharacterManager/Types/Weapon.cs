﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class Weapon
	{

		public string Name { get; set; }
		public WeaponType WeaponType { get; set; }
		public string Damage { get; set; }
	}
}
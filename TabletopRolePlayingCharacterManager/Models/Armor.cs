﻿using System;
using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Armor
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[NotNull, MaxLength(30)]
		public string ArmorType { get; set; }
		public int baseAC { get; set; }
		public int MagicBonus { get; set; }
		[Ignore]
		public int ArmorClass => MagicBonus + baseAC;


		public Armor(string name, string armorType, int armorClass, int magicLevel)
		{
			Name = name;
			ArmorType = armorType;
			baseAC = armorClass;
			MagicBonus = magicLevel;
		}

		public void UpdateStats(string armorType, int ac, int magicLevel)
		{
			if (armorType == null) throw new ArgumentNullException(nameof(armorType));
			ArmorType = armorType;
			baseAC = ac;
			MagicBonus = magicLevel;
		}


	}
}
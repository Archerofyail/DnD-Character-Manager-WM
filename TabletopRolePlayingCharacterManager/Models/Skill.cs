﻿using SQLite.Net.Attributes;
namespace TabletopRolePlayingCharacterManager.Models
{
	public class Skill
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; private set; }
		public string Name { get; private set; }
		public readonly MainStat MainStatType;
		public bool IsProficient { get; private set; }

		public Skill(string name, MainStat mainStatType, bool isProficient = false)
		{
			Name = name;
			MainStatType = mainStatType;
			IsProficient = isProficient;
		}

		public void ChangeProficiency(bool isProf)
		{
			IsProficient = isProf;
		}
	}

}
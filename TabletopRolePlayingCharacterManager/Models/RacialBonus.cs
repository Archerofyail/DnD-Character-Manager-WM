using System;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class RacialBonus
	{
		public string Race { get; set; }
		//Can be empty if no parent race exists 
		public string ParentRace { get; set; } = "";
		public MainStatType SpellStat { get; set; } = MainStatType.Intelligence;
		public List<List<Tuple<MainStatType, int>>> StatBonuses { get; } = new List<List<Tuple<MainStatType, int>>>();
		public List<List<Trait>> Traits { get; set; } = new List<List<Trait>>();
		public List<List<Skill>> Skills { get; set; } = new List<List<Skill>>();
		public List<List<string>> Languages { get; set; } = new List<List<string>>();
		public Dictionary<int, List<Spell>> Spells { get; set; } = new Dictionary<int, List<Spell>>();
		public List<List<Proficiency>> Proficiencies { get; set; } = new List<List<Proficiency>>();
		private int speedBonus = 30;

		public int SpeedBonus
		{
			get
			{
				return speedBonus;
			}
			set { speedBonus = value; }
		}
	}
}

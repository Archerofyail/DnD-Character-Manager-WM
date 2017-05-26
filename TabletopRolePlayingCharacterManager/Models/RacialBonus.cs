using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class RacialBonus
	{
		public string Race { get; set; }
		//Can be empty if no parent race exists 
		public string ParentRace { get; set; } = "";
		public MainStatType SpellStat { get; set; } = MainStatType.Intelligence;
		public List<List<AbilityScoreBonusModel>> StatBonuses { get; set; } = new List<List<AbilityScoreBonusModel>>();
		public List<List<Trait>> Traits { get; set; } = new List<List<Trait>>();
		public List<List<Skill>> Skills { get; set; } = new List<List<Skill>>();
		public Dictionary<int, List<Spell>> Spells { get; set; } = new Dictionary<int, List<Spell>>();
		public List<List<Proficiency>> Proficiencies { get; set; } = new List<List<Proficiency>>();
		private int speedBonus = 30;

		public int SpeedBonus
		{
			get => speedBonus;
			set => speedBonus = value;
		}
	}
}

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
		public List<List<string>> Skills { get; set; } = new List<List<string>>();
		public Dictionary<int, List<Spell>> Spells { get; set; } = new Dictionary<int, List<Spell>>();
		public List<List<Proficiency>> Proficiencies { get; set; } = new List<List<Proficiency>>();

		public int SpeedBonus { get; set; } = 30;
	}
}

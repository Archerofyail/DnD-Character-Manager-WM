using System;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class ClassBonus
	{
		public string ClassName { get; set; } = "";
		public string SubclassTitle { get; set; } = "";
		public int SubclassLevel { get; set; }
		public DieType HitDie { get; set; } = DieType.D8;
		public int StartingHP { get; set; }
		public int MinHPPerLevel { get; set; }
		public DieType HitDiePerLevel { get; set; }
		public int NumberOfSkills { get; set; }
		public Dictionary<int, int> SpellSlotsByLevel { get; set; } = new Dictionary<int, int>();
		public Dictionary<int, int> SpellsKnownByLevel { get; set; }
		public Dictionary<int, int> CantripsKnownByLevel { get; set; }
		public List<string> WeaponProficiencies { get; set; } = new List<string>();
		public List<string> Proficiencies { get; set; } = new List<string>();
		public List<MainStatType> SavingThrowProficiencies { get; set; } = new List<MainStatType>();
		public List<Dictionary<int, List<Trait>>> TraitsByLevel { get; set; } = new List<Dictionary<int, List<Trait>>>();
		public List<Skill> Skills { get; set; }
		public Dictionary<int, int> AbilityScoreImprovements { get; set; } = new Dictionary<int, int>();

		public List<List<Weapon>> WeaponChoices { get; set; } = new List<List<Weapon>>();
		public List<List<Item>> ItemChoices { get; set; } = new List<List<Item>>();

		public Dictionary<int, int> Expertise { get; set; } = new Dictionary<int, int>();
		public List<Subclass> SubClasses { get; set; } = new List<Subclass>();
	}
}

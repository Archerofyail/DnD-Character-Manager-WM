using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace TabletopRolePlayingCharacterManager.Models
{
	//todo: possibly make a "Pack" class that contain weapons, items, and armor
	public class ClassBonus
	{
		public string ClassName { get; set; } = "";
		public string SubclassTitle { get; set; } = "";
		public int SubclassLevel { get; set; }
		public DieType HitDie { get; set; } = DieType.D8;
		public int StartingHP { get; set; }
		public int MinHPPerLevel { get; set; }
		public int NumberOfSkills { get; set; }
		public MainStatType SpellcastingStat { get; set; } = MainStatType.Intelligence;
		public List<List<int>> SpellSlotIncreasesByLevel { get; set; } = new List<List<int>>();
		public Dictionary<int, int> SpellsKnownByLevel { get; set; }
		public Dictionary<int, int> CantripsKnownByLevel { get; set; }
		public List<Proficiency> WeaponProficiencies { get; set; } = new List<Proficiency>();
		public List<Proficiency> Proficiencies { get; set; } = new List<Proficiency>();
		public Dictionary<int, List<MainStatType>> SavingThrowProficiencies { get; set; } = new Dictionary<int, List<MainStatType>>();
		public Dictionary<int, List<List<Trait>>> TraitsByLevel { get; set; } = new Dictionary<int, List<List<Trait>>>();
		public List<Skill> Skills { get; set; }
		public Dictionary<int, int> AbilityScoreImprovements { get; set; } = new Dictionary<int, int>();

		public List<List<Item>> EquipmentChoices { get; set; } = new List<List<Item>>();
		public Dictionary<int, int> Expertise { get; set; } = new Dictionary<int, int>();
		public List<Subclass> SubClasses { get; set; } = new List<Subclass>();

		public ClassBonus()
		{
			for (int i = 0; i < 20; i++)
			{
				SpellSlotIncreasesByLevel.Add(new List<int>());
				for (int j = 0; j < 9; j++)
				{
					SpellSlotIncreasesByLevel[i].Add(0);
				}
			}
		}
		
	}
}

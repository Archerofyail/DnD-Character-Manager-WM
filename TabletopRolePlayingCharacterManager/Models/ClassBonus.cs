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
		public List<List<int>> SpellSlotsByLevel { get; set; } = new List<List<int>>();
		public List<int> SpellsKnownByLevel { get; set; } = new List<int>();
		public List<int> CantripsKnownByLevel { get; set; } = new List<int>();
		public List<Proficiency> Proficiencies { get; set; } = new List<Proficiency>();
		public List<MainStatType> SavingThrowProficiencies { get; set; } = new List<MainStatType>();
		public Dictionary<int, List<List<Trait>>> TraitsByLevel { get; set; } = new Dictionary<int, List<List<Trait>>>();
		public List<string> Skills { get; set; }
		public List<int> AbilityScoreImprovements { get; set; } = new List<int>();

		public List<List<Item>> EquipmentChoices { get; set; } = new List<List<Item>>();
		public List<int> Expertise { get; set; } = new List<int>();
		public List<Subclass> SubClasses { get; set; } = new List<Subclass>();

		public ClassBonus()
		{
			for (int i = 0; i < 20; i++)
			{
				SpellSlotsByLevel.Add(new List<int>());
				AbilityScoreImprovements.Add((i + 1) % 4 == 0 ? 4 : 0);
				SpellsKnownByLevel.Add(0);
				CantripsKnownByLevel.Add(0);
				for (int j = 0; j < 9; j++)
				{
					SpellSlotsByLevel[i].Add(0);
				}
			}
		}

	}
}

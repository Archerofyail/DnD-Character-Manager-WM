using Newtonsoft.Json;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	//To make racial bonuses have a class with methods to add stuff to a character, like points to ability modifiers and entries to features and stuff. Then make a list of (structs maybe?) that contains the data, with the delegate. When that race is selected, call the delegate, and pass it the data
	public class Character5E
	{
		
		public int id { get; set; }
		private int _equippedArmorId;
		public int Level { get; set; }
		public int Experience { get; set; }
		//Validation to occur in the text box during character creation
		public int Speed { get; set; }
		public int MaxHP { get; set; }
		public int CurrHP { get; set; }
		public int TempHP { get; set; }
		
		public bool[] DeathSaveFails { get; set; } = new bool[3];
		
		public bool[] DeathSaveSuccesses { get; set; } = new bool[3];
		
		#region Aesthetic Featues
		
		public string Name { get; set; }
		public string Race { get; set; }
		public string Class { get; set; }
		public string Alignment { get; set; }
		public int Age { get; set; }
		
		
		public string Height { get; set; }
		
		public string Weight { get; set; }
		
		public string Eyes { get; set; }
		
		public string Skin { get; set; }
		
		public string Hair { get; set; }
		#endregion

	
		
		public int ProficiencyBonus => Utility.CalculateProficiencyBonus(Level);

		public int ArmorClass { get; set; } = 10;

		
		public Dictionary<MainStat, int> AbilityScores { get; set; } = new Dictionary<MainStat, int>();
		public Dictionary<MainStat, int> AbilityScoreProficiencies { get; set; } = new Dictionary<MainStat, int>();
		#region Ignored
		[JsonIgnore]
		public Dictionary<MainStat, int> abilityModifiers { get; private set; }
		[JsonIgnore]
		public Dictionary<string, int> SkillMods { get; set; }
		#endregion


		public Character5E()
		{
			abilityModifiers = new Dictionary<MainStat, int>();
		}


		public void CalculateAbilityModifiers(bool recalculate = false)
		{
			if (recalculate)
			{
				abilityModifiers.Clear();
			}
			if (abilityModifiers == null)
			{
				abilityModifiers = new Dictionary<MainStat, int>();
			}
			foreach (var mainstat in AbilityScores)
			{
				abilityModifiers.Add(mainstat.Key, Utility.CalculateMainStatBonus(mainstat.Value));
			}
		}

		public void CalculateSkillBonuses(bool recalculate = false)
		{
			if (recalculate)
			{
				SkillMods.Clear();
			}
			if (SkillMods == null)
			{
				SkillMods = new Dictionary<string, int>();
			}
			if (abilityModifiers.Count == 0 || abilityModifiers == null)
			{
				CalculateAbilityModifiers();
			}
			
		}
	}
}

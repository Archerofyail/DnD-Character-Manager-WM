using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.Models
{
	//To make racial bonuses have a class with methods to add stuff to a character, like points to ability modifiers and entries to features and stuff. Then make a list of (structs maybe?) that contains the data, with the delegate. When that race is selected, call the delegate, and pass it the data
	public class Character5E
	{
		/*TODO: Add a generic Value Class. One that has a base amount, a mod, a temp score, and maybe also a temp mod value. 
		 * Combining those 4 should give the total value of whatever the heck it is, that's up for the thing using it to decide
		Or possibly, one that has a dictionary with the keys being the name of the value. 
		Editing these should open up a content dialog that shows all the values and lets the user edit them, 
		as they would take up way too much space otherwise */
		//TODO: Change AbilityScores to a class (Name, Value, Bonus)
		public int id { get; set; }
		public string Notes { get; set; }
		public string Campaign { get; set; }
		public int Level { get; set; } = 1;
		public int Experience { get; set; }
		//Validation to occur in the text box during character creation
		public int Speed { get; set; }
		public int MaxHP { get; set; }
		public int CurrHP { get; set; }
		public int TempHP { get; set; }
		public int Initiative => AbilityModifiers[MainStatType.Dexterity];

		public bool[] DeathSaveFails { get; set; } = new bool[3] {false, false, false};

		public bool[] DeathSaveSuccesses { get; set; } = new bool[3] {false, false, false};

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

		public string PersonalityTraits { get; set; }
		public string Ideals { get; set; }
		public string Bonds { get; set; }
		public string Flaws { get; set; }
		public List<Trait> Traits { get; set; } = new List<Trait>();

		public string God { get; set; }

		public string Backstory { get; set; }
		public string RelationshipsAndAllies { get; set; }
		

		public int ProficiencyBonus => Utility.CalculateProficiencyBonus(Level);

		public int ArmorClass { get; set; } = 10;


		public Dictionary<MainStatType, int> AbilityScores { get; set; } = new Dictionary<MainStatType, int>();
		public Dictionary<MainStatType, bool> AbilityScoreProficiencies { get; set; } = new Dictionary<MainStatType, bool>();
		#region Ignored
		[JsonIgnore]
		public Dictionary<MainStatType, int> AbilityModifiers { get; private set; } = new Dictionary<MainStatType, int>();
		#endregion

		public Dictionary<int, Tuple<int, int>> SpellSlots { get; set; } = new Dictionary<int, Tuple<int, int>>();
		public List<Skill> Skills { get; set; } = new List<Skill>();
		public List<Item> Inventory { get; set; } = new List<Item>();
		public List<Weapon> Weapons { get; set; } = new List<Weapon>();
		public List<Spell> Spells { get; set; } = new List<Spell>();
		public List<string> WeaponProficiencies { get; set; } = new List<string>();
		public List<string> ArmorProficiencies { get; set; } = new List<string>();
		public List<string> Proficiencies { get; set; } = new List<string>();
		public List<string> Languages { get; set;  } = new List<string>();

		public Character5E()
		{

		}

		public Character5E(bool createDefaultData = false)
		{
			if (createDefaultData)
			{
				AbilityScores = new Dictionary<MainStatType, int>
				{
					{MainStatType.Strength, 10},
					{MainStatType.Dexterity, 10},
					{MainStatType.Constitution, 10},
					{MainStatType.Intelligence, 10},
					{MainStatType.Wisdom, 10},
					{MainStatType.Charisma, 10},

				};
				AbilityScoreProficiencies = new Dictionary<MainStatType, bool>
				{
					{MainStatType.Strength, false},
					{MainStatType.Dexterity, false},
					{MainStatType.Constitution, false},
					{MainStatType.Intelligence, false},
					{MainStatType.Wisdom, false},
					{MainStatType.Charisma, false},

				};
				CalculateAbilityModifiers();
				Skills = new List<Skill>
				{
					new Skill("Acrobatics", AbilityModifiers[MainStatType.Dexterity], ProficiencyBonus, MainStatType.Dexterity, false),
					new Skill("Animal Handling", AbilityModifiers[MainStatType.Wisdom], ProficiencyBonus, MainStatType.Wisdom, false),
					new Skill("Arcana", AbilityModifiers[MainStatType.Intelligence], ProficiencyBonus, MainStatType.Intelligence, false),
					new Skill("Athletics", AbilityModifiers[MainStatType.Strength], ProficiencyBonus, MainStatType.Strength, false),
					new Skill("Deception", AbilityModifiers[MainStatType.Charisma], ProficiencyBonus, MainStatType.Charisma, false),
					new Skill("History", AbilityModifiers[MainStatType.Intelligence], ProficiencyBonus, MainStatType.Intelligence,
						false),
					new Skill("Insight", AbilityModifiers[MainStatType.Wisdom], ProficiencyBonus, MainStatType.Wisdom, false),
					new Skill("Intimidation", AbilityModifiers[MainStatType.Charisma], ProficiencyBonus, MainStatType.Charisma, false),
					new Skill("Investigation", AbilityModifiers[MainStatType.Intelligence], ProficiencyBonus, MainStatType.Intelligence,
						false),
					new Skill("Medicine", AbilityModifiers[MainStatType.Wisdom], ProficiencyBonus, MainStatType.Wisdom, false),
					new Skill("Nature", AbilityModifiers[MainStatType.Intelligence], ProficiencyBonus, MainStatType.Intelligence, false),
					new Skill("Perception", AbilityModifiers[MainStatType.Wisdom], ProficiencyBonus, MainStatType.Wisdom, false),
					new Skill("Performance", AbilityModifiers[MainStatType.Charisma], ProficiencyBonus, MainStatType.Charisma, false),
					new Skill("Persuasion", AbilityModifiers[MainStatType.Charisma], ProficiencyBonus, MainStatType.Charisma, false),
					new Skill("Religion", AbilityModifiers[MainStatType.Intelligence], ProficiencyBonus, MainStatType.Intelligence,
						false),
					new Skill("Sleight of Hand", AbilityModifiers[MainStatType.Dexterity], ProficiencyBonus, MainStatType.Dexterity,
						false),
					new Skill("Stealth", AbilityModifiers[MainStatType.Dexterity], ProficiencyBonus, MainStatType.Dexterity, false),
					new Skill("Survival", AbilityModifiers[MainStatType.Wisdom], ProficiencyBonus, MainStatType.Wisdom, false),
				};
				CalculateSkillBonuses();
			}


		}

		public Character5E(int id, bool createDefaultData) : this(createDefaultData)
		{
			this.id = id;
		}

		public void CalculateAbilityModifiers()
		{

			AbilityModifiers.Clear();
			if (AbilityModifiers == null)
			{
				AbilityModifiers = new Dictionary<MainStatType, int>();
			}
			foreach (var mainstat in AbilityScores)
			{
				AbilityModifiers.Add(mainstat.Key, Utility.CalculateMainStatBonus(mainstat.Value));
			}
		}

		public void CalculateSkillBonuses()
		{

			if (AbilityModifiers.Count == 0 || AbilityModifiers == null)
			{
				CalculateAbilityModifiers();
			}
			foreach (var skill in Skills)
			{
				skill.CalculateBonus(AbilityModifiers[skill.MainStat], ProficiencyBonus);

			}
		}
	}
}

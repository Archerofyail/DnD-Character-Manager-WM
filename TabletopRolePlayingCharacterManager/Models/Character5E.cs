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
		private int _equippedArmorId;
		public int Level { get; set; } = 1;
		public int Experience { get; set; }
		//Validation to occur in the text box during character creation
		public int Speed { get; set; }
		public int MaxHP { get; set; }
		public int CurrHP { get; set; }
		public int TempHP { get; set; }
		public int Initiative { get { return abilityModifiers[MainStat.Dexterity]; } }

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

		public string PersonalityTraits { get; set; }
		public string Ideals { get; set; }
		public string Bonds { get; set; }
		public string Flaws { get; set; }


		public int ProficiencyBonus => Utility.CalculateProficiencyBonus(Level);

		public int ArmorClass { get; set; } = 10;


		public Dictionary<MainStat, int> AbilityScores { get; set; } = new Dictionary<MainStat, int>();
		public Dictionary<MainStat, bool> AbilityScoreProficiencies { get; set; } = new Dictionary<MainStat, bool>();
		#region Ignored
		[JsonIgnore]
		public Dictionary<MainStat, int> abilityModifiers { get; private set; }
		#endregion

		public List<Skill> Skills { get; set; } = new List<Skill>();
		public List<Item> Inventory { get; set; }
		public List<Weapon> Weapons { get; set; }
		public List<Spell> Spells { get; set; }

		public Character5E()
		{
			AbilityScores = new Dictionary<MainStat, int>
			{
				{ MainStat.Strength, 10 },
				{ MainStat.Dexterity, 10 },
				{ MainStat.Constitution, 10 },
				{ MainStat.Intelligence, 10 },
				{ MainStat.Wisdom, 10 },
				{ MainStat.Charisma, 10 },

			};
			AbilityScoreProficiencies = new Dictionary<MainStat, bool>
			{
				{ MainStat.Strength, false },
				{ MainStat.Dexterity, false },
				{ MainStat.Constitution, false },
				{ MainStat.Intelligence, false },
				{ MainStat.Wisdom, false },
				{ MainStat.Charisma, false },

			};
			CalculateAbilityModifiers();
			Skills = new List<Skill>
			{
				new Skill("Acrobatics", abilityModifiers[MainStat.Dexterity], ProficiencyBonus, MainStat.Dexterity, false),
				new Skill("Animal Handling", abilityModifiers[MainStat.Wisdom], ProficiencyBonus, MainStat.Wisdom, false),
				new Skill("Arcana", abilityModifiers[MainStat.Intelligence], ProficiencyBonus, MainStat.Intelligence, false),
				new Skill("Athletics", abilityModifiers[MainStat.Strength], ProficiencyBonus, MainStat.Strength, false),
				new Skill("Deception", abilityModifiers[MainStat.Charisma], ProficiencyBonus, MainStat.Charisma, false),
				new Skill("History", abilityModifiers[MainStat.Intelligence], ProficiencyBonus, MainStat.Intelligence, false),
				new Skill("Insight", abilityModifiers[MainStat.Wisdom], ProficiencyBonus, MainStat.Wisdom, false),
				new Skill("Intimidation", abilityModifiers[MainStat.Charisma], ProficiencyBonus, MainStat.Charisma, false),
				new Skill("Investigation", abilityModifiers[MainStat.Intelligence], ProficiencyBonus, MainStat.Intelligence, false),
				new Skill("Medicine", abilityModifiers[MainStat.Wisdom], ProficiencyBonus, MainStat.Wisdom, false),
				new Skill("Nature", abilityModifiers[MainStat.Intelligence], ProficiencyBonus, MainStat.Intelligence, false),
				new Skill("Perception", abilityModifiers[MainStat.Wisdom], ProficiencyBonus, MainStat.Wisdom, false),
				new Skill("Performance", abilityModifiers[MainStat.Charisma], ProficiencyBonus, MainStat.Charisma, false),
				new Skill("Persuasion", abilityModifiers[MainStat.Charisma], ProficiencyBonus, MainStat.Charisma, false),
				new Skill("Religion", abilityModifiers[MainStat.Intelligence], ProficiencyBonus, MainStat.Intelligence, false),
				new Skill("Sleight of Hand", abilityModifiers[MainStat.Dexterity], ProficiencyBonus, MainStat.Dexterity, false),
				new Skill("Stealth", abilityModifiers[MainStat.Dexterity], ProficiencyBonus, MainStat.Dexterity, false),
				new Skill("Survival", abilityModifiers[MainStat.Wisdom], ProficiencyBonus, MainStat.Wisdom, false),
			};
			CalculateSkillBonuses();
		}

		public Character5E(int id) : this()
		{
			this.id = id;
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

		public void CalculateSkillBonuses()
		{

			if (abilityModifiers.Count == 0 || abilityModifiers == null)
			{
				CalculateAbilityModifiers();
			}
			foreach (var skill in Skills)
			{
				skill.CalculateBonus(abilityModifiers[skill.MainStat], ProficiencyBonus);

			}
		}
	}
}

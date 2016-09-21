using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.Models
{
	//To make racial bonuses have a class with methods to add stuff to a character, like points to ability modifiers and entries to features and stuff. Then make a list of (structs maybe?) that contains the data, with the delegate. When that race is selected, call the delegate, and pass it the data
	public class Character5E
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		private int equippedArmor;
		public int Level { get; set; }
		public int Experience { get; set; }
		[Ignore]
		public int ProficiencyBonus { get { return Utility.CalculateProficiencyBonus(Level); } }
		[ForeignKey(typeof(Race))]
		public string Race { get; set; }
		[ForeignKey(typeof(SubRace))]
		public string Subrace { get; set; }
		[ForeignKey(typeof(Class))]
		public string Class { get; set; }
		[ForeignKey(typeof(Subclass))]
		public string SubClass { get; set; }
		
		[Ignore]
		public int ArmorClass
		{
			get
			{
				return Armor.Find((item) => item.Equipped).ArmorClass +
					   Utility.CalculateMainStatBonus(mainstats[MainStat.Dexterity]);
			}
		}

		//Validation to occur in the text box during character creation
		public int Speed { get; set; }

		#region Aesthetic Featues
		[MaxLength(50)]
		public string Name { get; set; }
		public int Age { get; set; }
		[MaxLength(30)]
		public string Height { get; set; }
		[MaxLength(15)]
		public string Weight { get; set; }
		[MaxLength(30)]
		public string EyeColor { get; set; }
		[MaxLength(30)]
		public string SkinColor { get; set; }
		[MaxLength(30)]
		public string HairColor { get; set; }
		#endregion

		[TextBlob("MainStats")]
		public Dictionary<MainStat,int> MainStatsJson
		{
			get { return mainstats; }
			set { mainstats = value; }
		}
		//Stored as the full number, the Bonus will be calculated on the fly
		private Dictionary<MainStat, int> mainstats = new Dictionary<MainStat, int>
		{
			{ MainStat.Strength, 0},
			{ MainStat.Dexterity, 0 },
			{ MainStat.Constitution, 0 },
			{ MainStat.Intelligence, 0 },
			{ MainStat.Wisdom, 0 },
			{ MainStat.Charisma, 0 }

		};

		[ForeignKey(typeof(Alignment))]
		public Alignment Alignment { get; set; }

		[Ignore]
		public Dictionary<MainStat, int> abilityModifiers { get; private set; }

		public List<Skill> Skills { get; set; }
		[OneToMany]
		public List<SkillProficiency> SkillProficiencies { get; set; }
		public Dictionary<string, int> SkillMods { get; set; }

		//Todo: figure out how to load proficiencies as items from another table, that work with an ORM
		[ManyToMany(typeof(CharacterProficiency))]
		public List<Proficiency> Proficiencies { get; set; }

		
		public List<Proficiency> Features { get; private set; }
		[ManyToMany(typeof(CharacterItem))]
		public List<IItem> Items { get; set; }
		[ManyToMany(typeof(CharacterArmor))]
		public List<Armor> Armor { get; set; }
		[ManyToMany(typeof(CharacterWeapon))]
		public List<Weapon> Weapons { get; set; }
		[ManyToMany(typeof(CharacterSpell))]
		public List<Spell> Spells { get; set; }
		[OneToMany]
		public List<CharacterPreparedSpells> SpellsPrepared { get; set; }




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
			foreach (var mainstat in mainstats)
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
			foreach (var skill in Skills)
			{
				int bonus = SkillProficiencies.Find(x => x.Skill_id == skill.id).isProficient ? ProficiencyBonus : 0;
				bonus += abilityModifiers[skill.MainStatType];
				SkillMods.Add(skill.Name, bonus);
			}
		}
	}
}

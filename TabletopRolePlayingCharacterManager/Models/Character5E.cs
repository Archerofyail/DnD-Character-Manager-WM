using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using TabletopRolePlayingCharacterManager.Models.Intermediates;

namespace TabletopRolePlayingCharacterManager.Models
{
	//To make racial bonuses have a class with methods to add stuff to a character, like points to ability modifiers and entries to features and stuff. Then make a list of (structs maybe?) that contains the data, with the delegate. When that race is selected, call the delegate, and pass it the data
	public class Character5E
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		private int _equippedArmorId;
		public int Level { get; set; }
		public int Experience { get; set; }
		//Validation to occur in the text box during character creation
		public int Speed { get; set; }
		public int MaxHP { get; set; }
		public int CurrHP { get; set; }
		public int TempHP { get; set; }

		public string DeathSaveFailsJson
		{
			get { return JsonConvert.SerializeObject(DeathSaveFails); }
			set
			{
				if (value != null)
				{
					DeathSaveFails = JsonConvert.DeserializeObject<bool[]>(value);
				}
			}
		}

		public string DeathSaveSuccessesJson
		{
			get { return JsonConvert.SerializeObject(DeathSaveSuccesses); }
			set
			{
				if (value != null)
				{
					DeathSaveSuccesses = JsonConvert.DeserializeObject<bool[]>(value);
				}
			}
		}

		[Ignore]
		public bool[] DeathSaveFails { get; set; } = new bool[3];
		[Ignore]
		public bool[] DeathSaveSuccesses { get; set; } = new bool[3];
		
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

		#region Ignored
		[Ignore]
		public int ProficiencyBonus => Utility.CalculateProficiencyBonus(Level);
		[Ignore]
		public int ArmorClass
		{
			get
			{
				return Armor.Find((item) => item.id == _equippedArmorId).ArmorClass +
					   Utility.CalculateMainStatBonus(MainStats[MainStat.Dexterity]);
			}
		}

		[Ignore]
		public Dictionary<MainStat, int> MainStats { get; set; } = new Dictionary<MainStat, int>();

		[Ignore]
		public Dictionary<MainStat, int> abilityModifiers { get; private set; }
		[Ignore]
		public Dictionary<string, int> SkillMods { get; set; }
		#endregion


		public string MainStatsJson
		{
			get { return JsonConvert.SerializeObject(MainStats); }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					MainStats = JsonConvert.DeserializeObject<Dictionary<MainStat, int>>(value);
				}
			}
		}
		#region DatabaseRelationships

		[ForeignKey(typeof(Race))]
		public int RaceId { get; set; }
		[ManyToOne]
		public Race Race { get; set; }

		[ForeignKey(typeof(Subrace))]
		public int SubraceId { get; set; }
		[ManyToOne]
		public Subrace Subrace { get; set; }

		[ForeignKey(typeof(CharacterClass))]
		public int ClassId { get; set; }
		[ManyToOne]
		public CharacterClass CharacterClass { get; set; }

		[ForeignKey(typeof(CharacterSubclass))]
		public int SubClassId { get; set; }
		[ManyToOne]
		public CharacterSubclass CharacterSubclass { get; set; }
		//Stored as the full number, the Bonus will be calculated on the fly
		

		[ForeignKey(typeof(Alignment))]
		public int AlignmentId { get; set; }
		[ManyToOne]
		public Alignment Alignment { get; set; }


		[ManyToMany(typeof(CharacterSkill))]
		public List<Skill> Skills { get; set; }
		[OneToMany]
		public List<CharacterSkillProficiency> SkillProficiencies { get; set; }


		[ManyToMany(typeof(CharacterProficiency))]
		public List<Proficiency> Proficiencies { get; set; }
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
			foreach (var mainstat in MainStats)
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
				int bonus = SkillProficiencies.Find(x => x.SkillId == skill.id).IsProficient ? ProficiencyBonus : 0;
				bonus += abilityModifiers[skill.MainStatType];
				SkillMods.Add(skill.Name, bonus);
			}
		}
	}
}

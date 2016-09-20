using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace TabletopRolePlayingCharacterManager.Models
{
	//To make racial bonuses have a class with methods to add stuff to a character, like points to ability modifiers and entries to features and stuff. Then make a list of (structs maybe?) that contains the data, with the delegate. When that race is selected, call the delegate, and pass it the data
	class Character5E
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		private int equippedArmor;
		public int Level = 1;
		public int ProficiencyBonus { get { return Utility.CalculateProficiencyBonus(Level); } }
		public string Race { get; set; }
		public string Subrace { get; set; }
		public string Class { get; set; }
		public string SubClass { get; set; }

		public Alignment Alignment { get; set; }

		public int ArmorClass
		{
			get
			{
				return Armor.Find((item) => item.Equipped).ArmorClass +
					   Utility.CalculateMainStatBonus(mainstats[MainStat.Dexterity]);
			}
		}

		private int speed;
		public int Speed
		{
			get { return speed; }
			set { speed = (value/5)*5; }
		}

		public string Name { get; set; }
		public int Age { get; set; }
		public string Height { get; set; }
		public string Weight { get; set; }
		public string EyeColor { get; set; }
		public string SkinColor { get; set; }
		public string HairColor { get; set; }



		//Stored as the full number, the Bonus will be calculated on the fly
		public Dictionary<MainStat, int> mainstats = new Dictionary<MainStat, int>
		{
			{ MainStat.Strength, 0},
			{ MainStat.Dexterity, 0 },
			{ MainStat.Constitution, 0 },
			{ MainStat.Intelligence, 0 },
			{ MainStat.Wisdom, 0 },
			{ MainStat.Charisma, 0 }

		};

		public Dictionary<MainStat, int> abilityModifiers = null;

		private List<Skill> skills = new List<Skill>();

		public List<Skill> Skills { get { return skills; } }

		//Todo: figure out how to load proficiencies as items from another table, that work with an ORM
		private List<Proficiency> proficiencies = new List<Proficiency>();
		[ManyToMany(typeof(CharacterProficiency))]
		public List<Proficiency> Proficiencies { get { return proficiencies; } }
		private List<Proficiency> features = new List<Proficiency>();
		public List<Proficiency> Features { get { return features; } }
		public List<IItem> Items { get; set; }
		public List<Armor> Armor { get; set; } 
		public List<Weapon> Weapons { get; set; } 

		public void CalculateAbilityModifiers()
		{
			if (abilityModifiers == null)
			{
				abilityModifiers = new Dictionary<MainStat, int>();
			}
			foreach (var mainstat in mainstats)
			{
				abilityModifiers.Add(mainstat.Key, Utility.CalculateMainStatBonus(mainstat.Value));
			}
		}

		public void CalculateSkillBonuses()
		{
			if (abilityModifiers == null)
			{
				CalculateAbilityModifiers();
			}
			foreach (var skill in Skills)
			{
				
			}
		}
	}
}

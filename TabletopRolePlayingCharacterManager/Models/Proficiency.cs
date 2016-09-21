
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	//This is for non-skill proficiencies, i.e. languages, weapon and armor, tools, etc.
	public class Proficiency
	{
		[PrimaryKey(), AutoIncrement]
		private int id { get; set; }
		public string name;
		public string description;
		public string category = "Miscellaneous";
		[ManyToMany(typeof(CharacterProficiency))]
		public List<Character5E> Characters { get; set; }

		public string Name
		{
			get { return name; }
		}

		public string Description
		{
			get { return description; }
		}

		public string Category
		{
			get { return category; }
		}
	}
}
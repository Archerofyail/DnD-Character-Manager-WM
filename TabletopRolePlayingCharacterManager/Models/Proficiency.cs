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
		public string Name { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		[ManyToMany(typeof(CharacterProficiency))]
		public List<Character5E> Characters { get; set; }
	}
}
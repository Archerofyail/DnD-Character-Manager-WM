using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Subrace
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[ForeignKey(typeof(Race))]
		public int ParentRace_id { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Character5E> Characters { get; set; }

		public Subrace(string title, string description)
		{
			Name = title;
			Description = description;
			Characters = new List<Character5E>();
		}

		public Subrace()
		{
			Characters = new List<Character5E>();
		}

	}
}
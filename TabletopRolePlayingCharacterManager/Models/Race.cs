using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Race
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Character5E> Characters { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Subrace> Subraces { get; set; }

		public Race(string title, string description)
		{
			Name = title;
			Description = description;
			Characters = new List<Character5E>();
			Subraces = new List<Subrace>();
		}

		public Race()
		{
			Characters = new List<Character5E>();
			Subraces = new List<Subrace>();
		}

	}
}
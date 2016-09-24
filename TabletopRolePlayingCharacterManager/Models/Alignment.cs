using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Alignment
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }

		[OneToMany]
		public List<Character5E> Characters { get; set; }

	}
}

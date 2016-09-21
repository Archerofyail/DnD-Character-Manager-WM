using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Subrace
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[ForeignKey(typeof(Race))]
		public Race ParentRace { get; set; }
		public Subrace(string title, string description)
		{
			Name = title;
			Description = description;
		}

		public Subrace()
		{

		}

	}
}
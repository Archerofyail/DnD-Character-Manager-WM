using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Race
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Race(string title, string description)
		{
			Name = title;
			Description = description;
		}

		public Race()
		{

		}

	}
}
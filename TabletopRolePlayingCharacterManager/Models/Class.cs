using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Class
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		[NotNull]
		public string Name { get; set; }
		[NotNull]
		public string Description { get; set; }
		[NotNull]
		public int HitDieSize { get; set; }
		[NotNull]
		public int InitHP { get; set; }
		[NotNull]
		public string HPPerLevel { get; set; }
		public Class(string title, string description)
		{
			Name = title;
			Description = description;
		}

		public Class()
		{

		}

	}
}
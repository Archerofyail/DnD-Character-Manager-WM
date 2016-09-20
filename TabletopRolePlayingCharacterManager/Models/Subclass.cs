using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Subclass
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		[NotNull]
		public string Name { get; set; }
		[NotNull]
		public string Description { get; set; }
		
		public Class ParentClass { get; set; }
		public Subclass(string title, string description)
		{
			Name = title;
			Description = description;
		}

		public Subclass()
		{

		}

	}
}
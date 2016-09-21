using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

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
		[ForeignKey(typeof(Class))]
		public int ParentClass_id { get; set; }
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
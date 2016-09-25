using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

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
		public int ParentClassId { get; set; }

		[ManyToOne]
		public Class ParentClass { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Character5E> Characters { get; set; }
		public Subclass(string title, string description)
		{
			Name = title;
			Description = description;
			Characters = new List<Character5E>();
		}

		public Subclass()
		{
			Characters = new List<Character5E>();
		}

	}
}
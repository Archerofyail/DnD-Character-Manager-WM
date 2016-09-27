using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterSubclass
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		[NotNull]
		public string Name { get; set; }
		[NotNull]
		public string Description { get; set; }
		[ForeignKey(typeof(CharacterClass))]
		public int ParentClassId { get; set; }

		[ManyToOne]
		public CharacterClass ParentCharacterClass { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Character5E> Characters { get; set; }
		public CharacterSubclass(string title, string description)
		{
			Name = title;
			Description = description;
			Characters = new List<Character5E>();
		}

		public CharacterSubclass()
		{
			Characters = new List<Character5E>();
		}

	}
}
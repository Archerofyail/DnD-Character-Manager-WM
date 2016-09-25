using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

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
		public int InitHp { get; set; }
		[NotNull]
		public string HpPerLevel { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Character5E> Characters { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Subclass> Subclasses { get; set; }

		public Class(string title, string description)
		{
			Name = title;
			Description = description;
			Characters = new List<Character5E>();
			Subclasses = new List<Subclass>();
		}

		public Class()
		{
			Characters = new List<Character5E>();
			Subclasses = new List<Subclass>();
		}

	}
}
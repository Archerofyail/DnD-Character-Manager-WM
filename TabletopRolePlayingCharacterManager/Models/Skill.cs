using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Skill
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; private set; }
		public string Name { get; private set; }
		public readonly MainStat MainStatType;

		public Skill(string name, MainStat mainStatType)
		{
			Name = name;
			MainStatType = mainStatType;
		}

		[ManyToMany(typeof(Character))]
		public List<Character5E> Characters { get; set; }
	}

}

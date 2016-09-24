using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Skill
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public MainStat MainStatType { get; set; }

		public Skill(string name, MainStat mainStatType)
		{
			Name = name;
			MainStatType = mainStatType;
			Characters = new List<Character5E>();
		}

		public Skill()
		{
			Characters = new List<Character5E>();
		}

		[ManyToMany(typeof(CharacterSkill))]
		public List<Character5E> Characters { get; set; }
	}

}

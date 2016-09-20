using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Spell
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Damage { get; set; }
		[NotNull()]
		public int SpellLevel { get; set; }
		public Spell(string title, string description)
		{
			Name = title;
			Description = description;
		}

		public Spell()
		{

		}

	}
}
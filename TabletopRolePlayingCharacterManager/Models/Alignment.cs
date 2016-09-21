using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Alignment
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		public string alignment = "";

	}
}

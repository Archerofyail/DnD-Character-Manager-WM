using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{

	class Item
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		[NotNull]
		public string Name { get; set; }
		[NotNull]
		public string Description { get; set; }
	}
}
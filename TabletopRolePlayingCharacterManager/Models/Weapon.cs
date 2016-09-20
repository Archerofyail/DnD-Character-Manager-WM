using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{

	class Weapon
	{
		[PrimaryKey(), AutoIncrement]
		public int id { get; set; }
		[NotNull, MaxLength(50)]
		public string Name { get; set; }
		[NotNull, MaxLength(10000)]
		public string Description { get; set; }
		[NotNull]
		public int AttackBonus { get; set; }
		[NotNull]
		public int DamageDie { get; set; }
		public int DamageBonus { get; set; }
	}
}
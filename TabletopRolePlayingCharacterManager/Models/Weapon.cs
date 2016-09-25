using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using TabletopRolePlayingCharacterManager.Models.Intermediates;

namespace TabletopRolePlayingCharacterManager.Models
{

	public class Weapon
	{
		[PrimaryKey, AutoIncrement]
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
		[ManyToMany(typeof(CharacterWeapon))]
		public List<Character5E> Characters { get; set; }
	}
}
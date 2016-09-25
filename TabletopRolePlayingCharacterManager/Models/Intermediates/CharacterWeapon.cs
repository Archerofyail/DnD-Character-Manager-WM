using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models.Intermediates
{
	public class CharacterWeapon
	{
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		[ForeignKey(typeof(Weapon))]
		public int WeaponId { get; set; }
	}
}

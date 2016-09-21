using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterWeapon
	{
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		[ForeignKey(typeof(Weapon))]
		public int Weapon_id { get; set; }
	}
}

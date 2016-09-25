using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models.Intermediates
{
	public class CharacterArmor
	{
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		[ForeignKey(typeof(Armor))]
		public int ArmorId { get; set; }
	}
}

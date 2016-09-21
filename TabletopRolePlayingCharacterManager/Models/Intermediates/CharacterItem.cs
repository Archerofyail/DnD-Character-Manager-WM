using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterItem
	{
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		[ForeignKey(typeof(Item))]
		public int Item_id { get; set; }
	}
}

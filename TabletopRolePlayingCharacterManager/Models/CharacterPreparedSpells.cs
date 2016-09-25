using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterPreparedSpells
	{
		[ForeignKey(typeof(Spell))]
		public int SpellId { get; set; }
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		public bool isPrepared { get; set; }
	}
}

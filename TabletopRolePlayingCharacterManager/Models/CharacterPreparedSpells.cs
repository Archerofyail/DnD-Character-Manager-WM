using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterPreparedSpells
	{
		[ForeignKey(typeof(Spell))]
		public int Spell_id { get; set; }
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		public bool isPrepared { get; set; }
	}
}

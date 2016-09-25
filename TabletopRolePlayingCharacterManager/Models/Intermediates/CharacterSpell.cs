using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models.Intermediates
{
	public class CharacterSpell
	{
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		[ForeignKey(typeof(Spell))]
		public int SpellId { get; set; }
	}
}

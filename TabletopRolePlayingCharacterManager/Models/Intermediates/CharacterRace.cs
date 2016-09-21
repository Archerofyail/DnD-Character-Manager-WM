using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	class CharacterRace
	{
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		[ForeignKey(typeof(Proficiency))]
		public int Race_id { get; set; }
	}
}

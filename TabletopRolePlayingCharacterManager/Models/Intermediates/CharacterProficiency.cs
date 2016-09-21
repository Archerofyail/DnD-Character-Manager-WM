using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	class CharacterProficiency
	{
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		[ForeignKey(typeof(Proficiency))]
		public int Proficiency_id { get; set; }
		public bool isProficient { get; set; }
	}
}

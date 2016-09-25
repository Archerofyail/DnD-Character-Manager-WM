using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models.Intermediates
{
	class CharacterProficiency
	{
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		[ForeignKey(typeof(Proficiency))]
		public int ProficiencyId { get; set; }
		public bool isProficient { get; set; }
	}
}

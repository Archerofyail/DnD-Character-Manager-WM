using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterSkillProficiency
	{
		[ForeignKey(typeof(Skill))]
		public int SkillId { get; set; }
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		public bool IsProficient;
	}
}

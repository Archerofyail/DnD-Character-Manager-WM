using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models.Intermediates
{
	public class CharacterSkill
	{
		[ForeignKey(typeof(Character5E))]
		public int CharacterId { get; set; }
		[ForeignKey(typeof(Skill))]
		public int SkillId { get; set; }
	}
}

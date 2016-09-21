using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterSkill
	{
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		[ForeignKey(typeof(Skill))]
		public int Skill_id { get; set; }
	}
}

using SQLiteNetExtensions.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class CharacterSkillProficiency
	{
		[ForeignKey(typeof(Skill))]
		public int Skill_id { get; set; }
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		public bool isProficient;
	}
}

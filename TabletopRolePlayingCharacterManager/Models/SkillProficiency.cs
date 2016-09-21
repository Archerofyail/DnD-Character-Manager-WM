using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class SkillProficiency
	{
		[ForeignKey(typeof(Skill))]
		public int Skill_id { get; set; }
		[ForeignKey(typeof(Character5E))]
		public int Character_id { get; set; }
		public bool isProficient;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class AbilityScoreBonusModel
	{
		public MainStatType Stat { get; set; } = MainStatType.Strength;
		public int Bonus { get; set; } = 1;
	}
}

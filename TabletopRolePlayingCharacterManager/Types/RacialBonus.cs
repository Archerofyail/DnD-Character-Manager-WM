using System;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class RacialBonus
	{
		public string Race { get; }
		public List<Tuple<MainStat, int>> StatBonuses { get; } = new List<Tuple<MainStat, int>>();
		private int speedBonus = 30;

		public int SpeedBonus
		{
			get
			{
				return speedBonus;
			}
			set { speedBonus = value; }
		}
	}
}

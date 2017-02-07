using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class RacialBonus
	{
		public string Race { get; }
		public List<Tuple<MainStat, int>> StatBonuses { get; } = new List<Tuple<MainStat, int>>();
		private int speedBonus = 5;

		public int SpeedBonus
		{
			get
			{
				return speedBonus;
			}
		}
	}
}

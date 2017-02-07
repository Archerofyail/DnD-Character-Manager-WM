using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class Damage
	{
		//Die Dize probable shouldn't be more than a D12
		public Dictionary<DieType, int> Dice { get; set; } = new Dictionary<DieType, int>();
		public int Bonus { get; set; } = 0;
		public MainStat MainStat { get; set; } = MainStat.Strength;

		public Damage(MainStat mstat, int bonus)
		{
			MainStat = mstat;
			Bonus = bonus;
		}

		public int RollDamage()
		{
			int finalRoll = 0;
			foreach (var die in Dice)
			{
				var dieSize = int.Parse(die.Key.ToString().Substring(1));
				for (int i = 0; i < die.Value; i++)
				{
					finalRoll += Utility.Rand.Next(1, dieSize + 1);
				}
			}
			finalRoll += Bonus;
			return finalRoll;
		}
	}
}

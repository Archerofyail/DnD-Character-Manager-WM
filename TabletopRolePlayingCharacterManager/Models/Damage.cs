using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	//TODO: Add a dice picker
	public class Damage
	{
		//Die Dize probable shouldn't be more than a D12
		public Dictionary<DieType, int> Dice { get; set; } = new Dictionary<DieType, int>();
		public int Bonus { get; set; } = 0;

		public Damage(int bonus)
		{
			
			Bonus = bonus;
		}

		public Damage()
		{

		}
		//Override Bonus with bonus if it's set to a value
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

		public override string ToString()
		{
			var text = "";
			if (Dice.Count > 0)
			{
				foreach (var die in Dice)
				{
					text += die.Value + "d" + die.Key.ToString();
				}
			}
			return text;
		}
	}
}

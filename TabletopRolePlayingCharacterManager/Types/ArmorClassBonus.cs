using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.Types
{
	class ArmorClassBonus : IStatIncrease
	{
		private int bonus = 1;
		private static string bonusName = "AC";
		private Character5E character;
		public int Bonus
		{
			get { return bonus; }

			set { bonus = value; }
		}

		public string BonusName
		{
			get { return bonusName; }

			
		}

		public Character5E Character
		{
			get { return character; }

			set { character = value; }
		}

		public void AddBonus()
		{
			character.ArmorClass += bonus;
		}

		public void RemoveBonus()
		{
			character.ArmorClass -= bonus;
		}
	}
}

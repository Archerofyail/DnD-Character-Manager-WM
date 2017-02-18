using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class StatIncrease
	{
		Character5E Character { get; set; }
		private static string bonusName { get; set; }

		public string BonusName
		{
			get { return bonusName; }
		}

		public int Bonus { get; set; }

		public virtual void AddBonus()
		{
			
		}

		public virtual void RemoveBonus()
		{
			
		}
	}
}

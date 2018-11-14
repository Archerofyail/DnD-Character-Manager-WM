using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.Types
{
	public interface IItemEffect
	{
		ItemEffectType Type { get; set; }

		void Use(Character5E character);

		void ToString();
	}
}

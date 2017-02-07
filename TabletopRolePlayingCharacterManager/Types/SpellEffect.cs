using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class SpellEffect
	{
		public string Name { get; set; }
		public SpellEffectType EffectType { get; set; } = SpellEffectType.Enemy;
		public string Description { get; set; }
	}
}

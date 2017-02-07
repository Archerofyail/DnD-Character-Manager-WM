using System;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class ClassBonus
	{
		public List<Tuple<int, List<Trait>>> TraitsByLevel { get; set; } = new List<Tuple<int, List<Trait>>>();
	}
}

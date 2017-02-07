using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class ClassBonus
	{
		public List<Tuple<int, List<Trait>>> TraitsByLevel { get; set; } = new List<Tuple<int, List<Trait>>>();
	}
}

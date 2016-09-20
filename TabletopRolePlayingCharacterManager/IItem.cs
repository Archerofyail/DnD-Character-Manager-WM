using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager
{
	interface IItem
	{
		int id { get; set; }
		string Name { get; set; }
		string Description { get; set; }

	}
}

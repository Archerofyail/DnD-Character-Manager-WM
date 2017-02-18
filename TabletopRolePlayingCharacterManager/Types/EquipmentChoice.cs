using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Types
{/// <summary>
/// Holds equipment for choosing in character creation. If there's multiple items in one choice, they will be in the ContainedItems list of the item
/// </summary>
	public class EquipmentChoice
	{
		public List<Item> choices { get; set; } = new List<Item>();
		public EquipmentChoice()
		{
			
		}

		public EquipmentChoice(Item item1, Item item2)
		{
			choices.Add(item1);
			choices.Add(item2);
		}

		public EquipmentChoice(Item item1, Item item2, Item item3) : this(item1, item2)
		{
			choices.Add(item3);
		}
	}
}

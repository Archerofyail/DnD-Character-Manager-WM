using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	//TODO: Implement weight for the UI
	public class Item
	{

		public string Name { get; set; }
		public string Description { get; set; }
		public int Weight { get; set; }
		public List<Item> ContainedItems { get; set; } = new List<Item>();
	}
}

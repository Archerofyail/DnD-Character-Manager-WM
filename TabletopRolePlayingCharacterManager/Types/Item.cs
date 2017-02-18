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
		public ItemType ItemType { get; set; } = ItemType.Item;

		public Item()
		{
			
		}

		public Item(string name, string description = "", ItemType type = ItemType.Item)
		{
			Name = name;
			ItemType = type;
			Description = description;
		}

		public Item(ItemType type)
		{
			ItemType = type;
		}
	}
}

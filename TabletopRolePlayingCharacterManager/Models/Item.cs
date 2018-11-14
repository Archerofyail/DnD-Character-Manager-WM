using System.Collections.Generic;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.Models
{
	//TODO: Implement weight for the UI
	public class Item
	{

		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public string Weight { get; set; }
		public List<Item> ContainedItems { get; set; } = new List<Item>();
		public ItemType ItemType { get; set; } = ItemType.Item;
		public IItemEffect Effect { get; set; } = null;
		public Item()
		{
			
		}

		public Item(string name, string description = "", ItemType type = ItemType.Item, int quantity = 1)
		{
			Name = name;
			ItemType = type;
			Description = description;
			Quantity = quantity;
		}

		public Item(ItemType type)
		{
			ItemType = type;
		}
	}
}

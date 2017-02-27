using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ItemViewModel : GenericItemViewModel
	{
		public new Item Item { get; }

		public ItemViewModel(Item item)
		{
			Item = item;
		}

		public ItemViewModel()
		{
			Item = base.Item as Item;
		}

		public string Name
		{
			get { return Item.Name; }
			set
			{
				Item.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get { return Item.Description; }
			set
			{
				Item.Description = value;
				RaisePropertyChanged();
			}
		}
	}
}

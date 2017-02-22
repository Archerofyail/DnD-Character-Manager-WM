using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class ItemViewModel : GenericItemViewModel
	{
		private Item Item;

		public ItemViewModel(Item item)
		{
			this.Item = item;
		}

		public ItemViewModel()
		{
			Item = item as Item;
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

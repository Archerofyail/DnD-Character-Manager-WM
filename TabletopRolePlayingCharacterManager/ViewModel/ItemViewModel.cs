using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class ItemViewModel : ViewModelBase
	{
		private Item item;

		public ItemViewModel(Item item)
		{
			this.item = item;
		}
		public string Name
		{
			get { return item.Name; }
			set
			{
				item.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get { return item.Description; }
			set
			{
				item.Description = value;
				RaisePropertyChanged();
			}
		}
	}
}

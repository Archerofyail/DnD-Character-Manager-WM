using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ItemViewModel : GenericItemViewModel
	{
		public new Item Item { get; }

		public delegate void RemoveItemDelegate(ItemViewModel item);

		public RemoveItemDelegate RemoveAction;

		public ItemViewModel(Item item, RemoveItemDelegate remove = null)
		{
			Item = item;
			RemoveAction = remove;
		}

		public ItemViewModel()
		{
			Item = base.Item as Item;
		}

		public string Name
		{
			get => Item.Name;
			set
			{
				Item.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get => Item.Description;
			set
			{
				Item.Description = value;
				RaisePropertyChanged();
			}
		}

		public ICommand RemoveItem => new RelayCommand(RemoveItemEx);

		void RemoveItemEx()
		{
			RemoveAction?.Invoke(this);
		}
	}
}

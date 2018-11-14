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

		public bool HasDescription => Description.Length > 0;


		public ICommand RemoveItem => new RelayCommand(RemoveItemEx);

		void RemoveItemEx()
		{
			RemoveAction?.Invoke(this);
		}

		private bool isEditing;
		public bool IsEditing
		{
			get => isEditing;
			set
			{
				isEditing = value;
				RaisePropertyChanged();
				RaisePropertyChanged("IsNotEditing");
			}
		}

		public bool IsNotEditing => !IsEditing;

		public ICommand StartEditing => new RelayCommand(StartEditingEx);
		public ICommand StopEditing => new RelayCommand(StopEditingEx);
		void StartEditingEx()
		{
			IsEditing = true;
		}

		void StopEditingEx()
		{
			IsEditing = false;
			RaisePropertyChanged("LevelAndSchool");
			RaisePropertyChanged("AttackButtonText");
			RaisePropertyChanged("Description");
			RaisePropertyChanged("HigherLevels");
		}

	}
}

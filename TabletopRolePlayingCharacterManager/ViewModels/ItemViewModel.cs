using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

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

		public string Weight
		{
			get => Item.Weight;
			set
			{
				Item.Weight = value;
				RaisePropertyChanged();
				RaisePropertyChanged("TotalWeight");
			}

		}

		public string TotalWeight => (Utility.ParseWeight(Item.Weight) * Item.Quantity).ToString();

		public int Quantity
		{
			get => Item.Quantity;
			set
			{
				Item.Quantity = value;
				RaisePropertyChanged();
				RaisePropertyChanged("TotalWeight");
			}
		}

		public bool HasDescription => Description.Length > 0;

		public bool HasUse => Item.Effect != null;

		public  ICommand Use=> new RelayCommand(UseEx);

		public ICommand RemoveItem => new RelayCommand(RemoveItemEx);

		void UseEx()
		{
			if (Quantity <= 0)
				return;
			Quantity -= 1;
			Item.Effect?.Use(CharacterManager.CurrentCharacter);
		}

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

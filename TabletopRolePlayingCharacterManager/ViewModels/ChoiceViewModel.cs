using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	/// <summary>
	/// A class to display and deal with picking one or multiple items from a list
	/// </summary>
	/// <typeparam name="T">The type of the items</typeparam>
	/// <typeparam name="T2">The ViewModel type of the items</typeparam>
	public class ChoiceViewModel<T, T2> : ViewModelBase 
		where T2 : GenericItemViewModel, new()
		where T : class, new()
	{
		protected List<T> Items = new List<T>();
		private int _totalBonus = 1;

		public ChoiceViewModel(List<T> items, int totalBonus, bool canSelectMultiple)
		{
			this.Items = items;
			this._totalBonus = totalBonus;
			CanSelectMultiple = canSelectMultiple;
		}

		public ChoiceViewModel(List<T> items)
		{
			if (items != null)
			{
				this.Items = items;
			}

		}

		public ChoiceViewModel()
		{
			
		}

		public int TotalBonus
		{
			get => _totalBonus;
			set
			{
				_totalBonus = value;
				RaisePropertyChanged();
			}
		}

		public bool CanSelectMultiple { get; set; }

		private int _selectedIndex;

		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				_selectedIndex = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<T2> _selectedItems = new ObservableCollection<T2>();
		public ObservableCollection<T2> SelectedItems
		{
			get => _selectedItems;
			set => _selectedItems = value;
		}

		private ObservableCollection<T2> _choices = new ObservableCollection<T2>();

		public ObservableCollection<T2> Choices
		{
			get
			{
				if (_choices.Count == 0)
				{
					foreach (var item in Items)
					{
						var itm = new T2
						{
							Item = item
						};
						_choices.Add(itm);	
					}
				}
				return _choices;
			}

			protected set => _choices = value;
		}

		#region  Commands

		public T SelectedItem => Items[_selectedIndex];

		public ICommand AddItem => new RelayCommand(AddItemExec);

		protected virtual void AddItemExec()
		{
			Items.Add(new T());
		}

		/// <summary>
		/// Add an Item that the subclass has modified
		/// </summary>
		/// <param name="modItem"></param>
		protected void AddModifiedItem(T modItem)
		{
			Items.Add(modItem);
			_choices.Add(new T2 { Item = modItem });
		}

		#endregion

		public List<T> GetSelectedItems()
		{
			if (CanSelectMultiple)
			{
				return SelectedItems.Select<T2, T>(x => { return x.Item as T; }).ToList();
			}
			else
			{
				return new List<T>(new [] {SelectedItem});
			}
		}

	}
}

/*
 * To make this successfully generic I need to find a way to get a solid object with the known type
 * Data models to use with this: Proficiencies, Traits, Items,
 * 
 * */
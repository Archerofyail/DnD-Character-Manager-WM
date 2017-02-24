using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
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
		where T : new()
	{
		private List<T> items = new List<T>();
		private int totalBonus = 1;

		public ChoiceViewModel(List<T> items, int totalBonus, bool canSelectMultiple)
		{
			this.items = items;
			this.totalBonus = totalBonus;
			this.CanSelectMultiple = canSelectMultiple;
		}

		public ChoiceViewModel(List<T> items)
		{
			if (items != null)
			{
				this.items = items;
			}

		}

		public ChoiceViewModel()
		{
			items.Add(new T());
		}

		public int TotalBonus
		{
			get { return totalBonus; }
			set
			{
				totalBonus = value;
				RaisePropertyChanged();
			}
		}

		public bool CanSelectMultiple { get; set; } = false;

		private int selectedIndex = -1;

		public int SelectedIndex
		{
			get { return selectedIndex; }
			set
			{
				selectedIndex = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<T2> choices = new ObservableCollection<T2>();

		public ObservableCollection<T2> Choices
		{
			get
			{
				if (choices.Count == 0)
				{
					foreach (var item in items)
					{
						var itm = new T2
						{
							Item = item
						};
						choices.Add(itm);	
					}
				}
				return choices;
			}

			protected set { choices = value; }
			
		}

		public T SelectedItem => items[selectedIndex];

		public ICommand AddItem => new RelayCommand(AddItemExec);

		protected virtual void AddItemExec()
		{
			items.Add(new T());
		}

		/// <summary>
		/// Add an Item that the subclass has modified
		/// </summary>
		/// <param name="modItem"></param>
		protected void AddModifiedItem(T modItem)
		{
			items.Add(modItem);
			choices.Add(new T2{Item = modItem});
		}
	}
}

/*
 * To make this successfully generic I need to find a way to get a solid object with the known type
 * Data models to use with this: Proficiencies, Traits, Items,
 * 
 * */
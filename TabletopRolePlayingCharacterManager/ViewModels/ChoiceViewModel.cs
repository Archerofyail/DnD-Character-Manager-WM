using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModels
{

	public class ChoiceViewModel<T, T2> : ViewModelBase 
		where T2 : GenericItemViewModel, new()
	{
		private List<T> items = new List<T>();
		private int totalBonus = 1;
		private bool canSelectMultiple = false;

		public ChoiceViewModel(List<T> items, int totalBonus, bool canSelectMultiple)
		{
			this.items = items;
			this.totalBonus = totalBonus;
			this.canSelectMultiple = canSelectMultiple;
		}

		public ChoiceViewModel(List<T> items)
		{
			this.items = items;
			
		}

		public ChoiceViewModel()
		{
			
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

		public bool CanSelectMultiple
		{
			get { return canSelectMultiple; }
			set { canSelectMultiple = value; }
		}

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
						var itm = new T2()
						{
							item = item
						};
						choices.Add(itm);	
					}
				}
				return choices;
			}
			
		}

		public T SelectedItem => items[selectedIndex];
	}
}

/*
 * To make this successfully generic I need to find a way to get a solid object with the known type
 * Data models to use with this: Proficiencies, Traits, Items,
 * 
 * */
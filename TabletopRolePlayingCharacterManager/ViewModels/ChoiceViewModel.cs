using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModel
{

	public class ChoiceViewModel<T, T2> : ViewModelBase 
		where T2 : GenericItemViewModel, new()
	{
		private List<T> items;
		
		public ChoiceViewModel(List<T> items)
		{
			this.items = items;
			
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

		public T SelectedItem
		{
			get { return items[selectedIndex]; }
		}
	}
}

/*
 * To make this successfully generic I need to find a way to get a solid object with the known type
 * Data models to use with this: Proficiencies, Traits, Items,
 * 
 * */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class PrimitiveListViewModel<T> : ViewModelBase where T : IConvertible
	{
		public List<T> Items { get; set; } = new List<T>();
		public int Count => Items.Count;
		public int SelectionCount { get; set; } = 1;
		public bool CanSelectMultiple { get; set; }
		public PrimitiveListViewModel()
		{
			
		}

		public PrimitiveListViewModel(List<T> items)
		{
			if (items != null)
			{
				Items = items;
			}
		}


		public PrimitiveListViewModel( int selectionCount, bool canSelectMultiple, List<T> items = null) : this(items)
		{
			SelectionCount = selectionCount;
			CanSelectMultiple = canSelectMultiple;
		}

		public void Add(T item)
		{
			Items.Add(item);
		}

		public void AddRange(List<T> items)
		{
			Items.AddRange(items);
		}
	}
}

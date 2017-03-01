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

		public PrimitiveListViewModel()
		{
			
		}

		public PrimitiveListViewModel(List<T> items)
		{
			Items = items;
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

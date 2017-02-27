using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class PrimitiveListViewModel<T> : ViewModelBase where T : IConvertible
	{
		public List<T> items { get; set; } = new List<T>();

		public PrimitiveListViewModel()
		{
			
		}

		public PrimitiveListViewModel(List<T> items)
		{
			this.items = items;
		}
	}
}

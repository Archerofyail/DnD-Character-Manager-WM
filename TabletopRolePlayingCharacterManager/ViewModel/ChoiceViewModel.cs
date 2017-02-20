using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{

	public class ChoiceViewModel<T, T2> : ViewModelBase 
		where T2 : GenericItemViewModel, new()
		where T : IEnumerable
	{
		private T items;
		
		public ChoiceViewModel(T items)
		{
			this.items = items;
			
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
						var itm = new T2();
						itm.item = item;
						
						choices.Add(itm);	
					}
				}
				return choices;
			}
			
		}
	}
}

/*
 * To make this successfully generic I need to find a way to get a solid object with the known type
 * Data models to use with this: Proficiencies, Traits, Items,
 * 
 * */
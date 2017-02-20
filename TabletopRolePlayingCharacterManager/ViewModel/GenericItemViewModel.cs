using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class GenericItemViewModel : ViewModelBase
	{ 
		public object item;

		public GenericItemViewModel(object itm)
		{
			itm = item;
		}

		public GenericItemViewModel()
		{
			
		}
	}
}

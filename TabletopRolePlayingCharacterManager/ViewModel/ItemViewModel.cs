using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	class ItemViewModel : ViewModelBase
	{
		private Item item;
		public string Name
		{
			get { return item.Name; }
			set
			{
				item.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get { return item.Description; }
			set
			{
				item.Description = value;
				RaisePropertyChanged();
			}
		}
	}
}

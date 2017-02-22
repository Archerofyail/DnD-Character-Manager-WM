using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class GenericItemViewModel : ViewModelBase
	{ 
		public object item;

		public GenericItemViewModel(object itm)
		{
			item = itm;
		}

		public GenericItemViewModel()
		{
			
		}
	}
}

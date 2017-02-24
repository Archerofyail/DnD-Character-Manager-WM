using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class GenericItemViewModel : ViewModelBase
	{ 
		public object Item;

		public GenericItemViewModel(object itm)
		{
			Item = itm;
		}

		public GenericItemViewModel()
		{
			
		}
	}
}

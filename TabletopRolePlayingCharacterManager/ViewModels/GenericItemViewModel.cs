using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class GenericItemViewModel : ViewModelBase
	{ 
		public virtual object Item { get; set; }

		public GenericItemViewModel(object itm)
		{
			Item = itm;
		}

		public GenericItemViewModel()
		{
			
		}
	}
}

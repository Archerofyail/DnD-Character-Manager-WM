using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	internal class ViewModelBase
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged([CallerMemberName]string senderName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(senderName));
			}
		}
	}
}
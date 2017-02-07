using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class SpellViewModel : ViewModelBase
	{
		private Spell spell;

		public SpellViewModel(Spell sp)
		{
			spell = sp;
		}

		public string Name
		{
			get { return spell.Name; }
			set
			{
				spell.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Level
		{
			get { return spell.Level.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					spell.Level = result;
				}
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get { return spell.Description; }
			set
			{
				spell.Description = value;
				RaisePropertyChanged();
			}
		}


	}
}

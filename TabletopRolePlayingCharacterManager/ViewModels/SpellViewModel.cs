using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class SpellViewModel : ViewModelBase
	{
		private Spell spell;
		private static ObservableCollection<string> _levels = new ObservableCollection<string> { "Cantrip", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
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

		private int selectedLevel = -1;

		public int SelectedLevel
		{
			get { return selectedLevel; }
			set
			{
				if (value < _levels.Count)
				{
					selectedLevel = value;
					if (_levels[value] == "Cantrip")
					{
						spell.Level = 0;
						RaisePropertyChanged();
						return;
					}
					spell.Level = int.Parse(_levels[value]);
					RaisePropertyChanged();

				}
			}
		}
		public ObservableCollection<string> Levels
		{
			get { return _levels; }
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

		public bool HasVerbalComponent
		{
			get { return spell.HasVerbalComponent; }
			set
			{
				spell.HasVerbalComponent = value;
				RaisePropertyChanged();
			}
		}

		public bool HasSomaticComponent
		{
			get { return spell.HasSomaticComponent; }
			set
			{
				spell.HasSomaticComponent = value;
				RaisePropertyChanged();
			}
		}

		public string MaterialComponent
		{
			get { return spell.MaterialComponent; }
			set { spell.MaterialComponent = value; }
		}


	}
}

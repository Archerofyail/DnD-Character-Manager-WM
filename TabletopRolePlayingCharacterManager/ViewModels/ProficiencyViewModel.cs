using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyViewModel : GenericItemViewModel
	{
		//private Proficiency proficiency;
		public override object Item { get; set; } = new Proficiency();

		//public delegate void removeProfDelegate(ProficiencyViewModel prof);

		//private removeProfDelegate removeProficiencyAction;
		private ObservableCollection<ProficiencyViewModel> List;
		public ProficiencyViewModel(Proficiency prof, ObservableCollection<ProficiencyViewModel> list)
		{
			Item = prof;
			//removeProficiencyAction = removeAction;
			List = list;
		}

		public ProficiencyViewModel()
		{
			//Item = new Proficiency();
		}

		public string Name
		{
			get => ((Proficiency)Item).Name;
			set
			{
				((Proficiency)Item).Name = value;
				RaisePropertyChanged();
			}
		}

		public ICommand RemoveProficiency => new RelayCommand(RemoveProficiencyEx);

		void RemoveProficiencyEx()
		{
			List.Remove(this);
		}
	}
}

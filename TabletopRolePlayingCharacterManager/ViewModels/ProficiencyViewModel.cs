﻿using System.Collections.Generic;
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

		public delegate void RemoveProfDelegate(ProficiencyViewModel prof);

		private RemoveProfDelegate removeProficiencyAction;
		private ObservableCollection<ProficiencyViewModel> list;
		public ProficiencyType Type => (Item as Proficiency).Type;
		public ProficiencyViewModel(Proficiency prof, RemoveProfDelegate removeAction)
		{
			Item = prof;
			removeProficiencyAction = removeAction;

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
			removeProficiencyAction?.Invoke(this);
		}
	}
}

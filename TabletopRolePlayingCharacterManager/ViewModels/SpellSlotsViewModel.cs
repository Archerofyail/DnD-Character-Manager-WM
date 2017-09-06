using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class SpellSlotsViewModel : ViewModelBase
	{
		public Pair<int, int> SpellSlot { get; }

		public string SlotLevel { get; set; }

		public int Slots
		{
			get => SpellSlot.Item1;
			set
			{
				SpellSlot.Item1 = value;
				RaisePropertyChanged();
			}
		}

		public int MaxSlots
		{
			get => SpellSlot.Item2;
			set
			{
				SpellSlot.Item2 = value;
				RaisePropertyChanged();
			}
		}

		public SpellSlotsViewModel(Pair<int, int> spellSlot, int slotLevel)
		{
			SlotLevel = "Level " + slotLevel;
			SpellSlot = spellSlot;
		}

		public ICommand AddSlot => new RelayCommand(AddSlotEx);
		public ICommand AddMaxSlot => new RelayCommand(AddMaxSlotEx);
		public ICommand MinusSlot => new RelayCommand(MinusSlotEx);
		public ICommand MinusMaxSlot => new RelayCommand(MinusMaxSlotEx);

		void AddSlotEx()
		{
			Slots++;
		}

		void MinusSlotEx()
		{
			Slots--;
		}

		void AddMaxSlotEx()
		{
			MaxSlots++;
		}

		void MinusMaxSlotEx()
		{
			MaxSlots--;
		}
	}
}

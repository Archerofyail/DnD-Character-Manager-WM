using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class TraitViewModel : GenericItemViewModel
	{
		public override object Item { get; set; } = new Trait();
		public Trait Trait => Item as Trait;

		public delegate void RemoveActionDelegate(TraitViewModel trait);

		public RemoveActionDelegate RemoveAction;

		public TraitViewModel(Trait trait, RemoveActionDelegate remove)
		{
			RemoveAction = remove;
			Item = trait;
		}

		public TraitViewModel()
		{

		}

		public bool IsActive
		{
			get => Trait.IsActive;
			set
			{
				Trait.IsActive = value;
				BonusButtonText = value ? "Disable Bonus" : "Enable Bonus";
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get => Trait.Description;
			set
			{
				Trait.Description = value;
				RaisePropertyChanged();
			}
		}

		private string bonusButtonText = "Enable Bonus";

		public string BonusButtonText
		{
			get => bonusButtonText;
			set
			{
				bonusButtonText = value;
				RaisePropertyChanged();
			}
		}

		private bool isEditing;
		public bool IsEditing
		{
			get => isEditing;
			set
			{
				isEditing = value;
				RaisePropertyChanged();
				RaisePropertyChanged("IsNotEditing");
			}
		}
		public bool IsNotEditing => !isEditing;

		public ICommand StartEditing => new RelayCommand(StartEditingEx);
		public ICommand StopEditing => new RelayCommand(StopEditingEx);
		public ICommand StopEditingEnter => new RelayCommand<KeyRoutedEventArgs>(StopEditingEnterEx);
		void StartEditingEx()
		{
			IsEditing = true;

		}

		void StopEditingEnterEx(KeyRoutedEventArgs args)
		{
			if (args.Key == VirtualKey.Enter)
			{
				IsEditing = false;
				RaisePropertyChanged("Description");
			}
		}

		void StopEditingEx()
		{
			IsEditing = false;
			RaisePropertyChanged("Description");
		}

		void ApplyBonus()
		{
			IsActive = true;
			Trait.StatBonus.AddBonus();
		}

		void RemoveBonus()
		{
			IsActive = false;
			Trait.StatBonus.RemoveBonus();
		}

		public ICommand RemoveTrait => new RelayCommand(RemoveTraitEx);

		void RemoveTraitEx()
		{
			RemoveAction?.Invoke(this);
		}

	}
}

using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class TraitViewModel : GenericItemViewModel
	{
		public override object Item { get; set; } = new Trait();
		private Trait trait => Item as Trait;

		public TraitViewModel(Trait trait)
		{
			Item = trait;
		}

		public TraitViewModel()
		{
			
		}

		public bool IsActive
		{
			get => trait.IsActive;
			set
			{
				trait.IsActive = value;
				BonusButtonText = value ? "Disable Bonus" : "Enable Bonus";
				RaisePropertyChanged();
			}
		}

		public string Description
		{
			get => trait.Description;
			set
			{
				trait.Description = value; 
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

		void ApplyBonus()
		{
			IsActive = true;
			trait.StatBonus.AddBonus();
		}

		void RemoveBonus()
		{
			IsActive = false;
			trait.StatBonus.RemoveBonus();
		}

	}
}

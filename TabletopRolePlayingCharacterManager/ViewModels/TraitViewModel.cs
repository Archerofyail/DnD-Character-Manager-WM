using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class TraitViewModel : GenericItemViewModel
	{
		private Trait trait;
		
		public TraitViewModel(Trait trait)
		{
			this.trait = trait;
		}

		public TraitViewModel()
		{
			trait = item as Trait;
		}

		public bool IsActive
		{
			get { return trait.IsActive; }
			set
			{
				trait.IsActive = value;
				BonusButtonText = value ? "Disable Bonus" : "Enable Bonus";
				RaisePropertyChanged();
			}
		}

		public string Trait
		{
			get { return trait.Description; }
			set
			{
				trait.Description = value; 
				RaisePropertyChanged();
			}
		}

		private string bonusButtonText = "Enable Bonus";

		public string BonusButtonText
		{
			get { return bonusButtonText; }
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

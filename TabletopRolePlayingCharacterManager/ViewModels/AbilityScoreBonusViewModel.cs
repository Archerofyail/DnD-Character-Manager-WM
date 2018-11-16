using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class AbilityScoreBonusViewModel : GenericItemViewModel
	{
		private AbilityScoreBonusModel _bonusModel = new AbilityScoreBonusModel();
		public int Bonus
		{
			get => _bonusModel.Bonus;
			set
			{
				_bonusModel.Bonus = value; 
				RaisePropertyChanged();
				
			}
		}

		public MainStatType MainStat
		{
			get => ((AbilityScoreBonusModel)Item).Stat;
			set => ((AbilityScoreBonusModel)Item).Stat = value;
		}

		public string BonusString => "+" + Bonus + " " + MainStat;
	}
}

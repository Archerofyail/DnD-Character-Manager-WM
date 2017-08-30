using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class AbilityScoreBonusViewModel : GenericItemViewModel
	{
		private AbilityScoreBonusModel bonusModel = new AbilityScoreBonusModel();
		public int Bonus
		{
			get => bonusModel.Bonus;
			set
			{
				bonusModel.Bonus = value; 
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

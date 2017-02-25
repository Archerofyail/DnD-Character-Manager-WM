using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class AbilityScoreBonusViewModel : GenericItemViewModel
	{
		private AbilityScoreBonusModel bonusModel = new AbilityScoreBonusModel();
		public int Bonus
		{
			get { return bonusModel.Bonus; }
			set
			{
				bonusModel.Bonus = value; 
				RaisePropertyChanged();
				
			}
		}

		public MainStatType MainStat
		{
			get { return ((AbilityScoreBonusModel)Item).Stat; }
			set { ((AbilityScoreBonusModel)Item).Stat = value; }
		}

		public string BonusString => "+" + Bonus + " " + MainStat;
	}
}

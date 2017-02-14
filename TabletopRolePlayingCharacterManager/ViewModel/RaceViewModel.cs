using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class RaceViewModel : ViewModelBase
	{
		private string name;
		private RacialBonus racialBonuses;

		public RaceViewModel(string name, RacialBonus raceBonuses)
		{
			this.name = name;
			racialBonuses = raceBonuses;
		}

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				RaisePropertyChanged();
			}
		}

		public string StatBonuses
		{
			get
			{
				var text = "";
				foreach (var bonusList in racialBonuses.StatBonuses)
				{
					foreach (var bonus in bonusList)
					{

						text += "+" + bonus.Item2 + bonus.Item1.ToString() + ", ";


					}

				}
				text.Substring(0, text.Length - 2);
				return text;
			}
		}

		public string Speed
		{
			get { return racialBonuses.SpeedBonus.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					if (result % 5 == 0)
					{
						racialBonuses.SpeedBonus = result;
						RaisePropertyChanged();
					}
				}
			}
		}

	}
}

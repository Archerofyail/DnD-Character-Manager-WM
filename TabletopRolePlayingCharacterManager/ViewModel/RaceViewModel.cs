using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class RaceViewModel : ViewModelBase
	{
		private RacialBonus racialBonuses;

		public RaceViewModel(RacialBonus raceBonuses)
		{
			racialBonuses = raceBonuses;
		}

		public string Name
		{
			get { return racialBonuses.Race; }
			set
			{
				racialBonuses.Race = value;
				RaisePropertyChanged();
			}
		}

		#region Lists

		#region Proficiencies

		public ObservableCollection<List<Proficiency>> WeaponProficiencies
		{
			get
			{
				var profs = new ObservableCollection<List<Proficiency>>();
				foreach (var proficiency in racialBonuses.Proficiencies.Where((x) => x[0].Type == ProficiencyType.Weapon))
				{
					profs.Add(proficiency);
				}
				return profs;
			}
		}

		#endregion

		#endregion
		#region VisualOnly

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

		#endregion


	}
}

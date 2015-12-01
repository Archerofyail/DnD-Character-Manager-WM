using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DnD_Character_Manager.Model
{
	class CharacterModel5E
	{
		//Stored as the full number, the Bonus will be calculated on the fly
		public Dictionary<MainStat, int> mainstats = new Dictionary<MainStat, int>
		{
			{ MainStat.Strength, 0},
			{ MainStat.Dexterity, 0 },
			{ MainStat.Constitution, 0 },
			{ MainStat.Intelligence, 0 },
			{ MainStat.Wisdom, 0 },
			{ MainStat.Charisma, 0 }

		};

		private ObservableCollection<Tuple<string, int, MainStat>> skills = new ObservableCollection<Tuple<string, int, MainStat>>();

		public ObservableCollection<Tuple<string, int, MainStat>> Skills { get { return skills; } }
		
	}
}

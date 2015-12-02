using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Character_Manager.ViewModel
{
	class AddNewCharacterPageViewModel
	{
		private ObservableCollection<Skill> skills = new ObservableCollection<Skill>();
		public ObservableCollection<Skill> Skills { get { return skills; } }

		public AddNewCharacterPageViewModel()
		{
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
		}
	}
}

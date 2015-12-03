using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnD_Character_Manager.Types;

namespace DnD_Character_Manager.ViewModel
{
	class AddNewCharacterPageViewModel
	{
		private ObservableCollection<Skill> skills = new ObservableCollection<Skill>();
		public ObservableCollection<Skill> Skills { get { return skills; } }
		private ObservableCollection<CharacterTrait> languages = new ObservableCollection<CharacterTrait>();

		public ObservableCollection<CharacterTrait> Languages
		{
			get
			{
				if (languages.Count == 0)
				{
					if (CharacterTraitSelectionStore.Languages.Count > 0)
					{
						languages = new ObservableCollection<CharacterTrait>(CharacterTraitSelectionStore.Languages);
					}
					languages.Add(new CharacterTrait("Common"));
					languages.Add(new CharacterTrait("Dwarven"));
				}
				return languages;
			}
		}

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

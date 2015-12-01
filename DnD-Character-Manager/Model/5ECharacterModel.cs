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
		public int proficiencyBonus { get; private set; }
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

		public Dictionary<MainStat, int> abilityModifiers = null;

		private ObservableCollection<Skill> skills = new ObservableCollection<Skill>();

		public ObservableCollection<Skill> Skills { get { return skills; } }



		public void CalculateAbilityModifiers()
		{
			if (abilityModifiers == null)
			{
				abilityModifiers = new Dictionary<MainStat, int>();
			}
			foreach (var mainstat in mainstats)
			{
				abilityModifiers.Add(mainstat.Key, Utility.CalculateMainStatBonus(mainstat.Value));
			}
		}

		public void CalculateSkillBonuses()
		{
			if (abilityModifiers == null)
			{
				CalculateAbilityModifiers();
			}
			foreach (var skill in Skills)
			{
				skill.CalculateBonus(mainstats[skill.MainStatType], proficiencyBonus);
			}
		}
	}
}

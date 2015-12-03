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

		private ObservableCollection<CharacterTrait> weaponProficiencies = new ObservableCollection<CharacterTrait>();

		public ObservableCollection<CharacterTrait> WeaponProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					weaponProficiencies.Add(new CharacterTrait("Simple"));
					weaponProficiencies.Add(new CharacterTrait("Warhammer"));
				}
				return weaponProficiencies;
			}
		}

		private ObservableCollection<CharacterTrait> armorProficiencies = new ObservableCollection<CharacterTrait>();
		public ObservableCollection<CharacterTrait> ArmorProficiencies { get {
			if (armorProficiencies.Count == 0)
			{
				armorProficiencies.Add(new CharacterTrait("Light"));
				armorProficiencies.Add(new CharacterTrait("Medium"));
			}
			return armorProficiencies; } }

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

using System;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Subclass
	{
		public string Name { get; set; } 
		public int NumOfSkillsProficiency { get; set; }
		public bool EnablesMagic { get; set; }
		public int MagicStartLevel { get; set; }
		/// <summary>
		/// If true, it means that the spells are added to the character sheet.
		/// If false, they're just added to the list of spells to choose from when changing prepared or known spells
		/// </summary>
		public bool AreSpellsLearned { get; set; }

		public List<Tuple<int, List<string>>> SkillProficiencies { get; set; } = new List<Tuple<int, List<string>>>();
		public List<Tuple<int, List<Spell>>> SpellsLearned { get; set; } = new List<Tuple<int, List<Spell>>>();
		public List<List<Trait>> TraitsLearned { get; set; } = new List<List<Trait>>();
		public List<List<Proficiency>> LanguagesLearned { get; set; } = new List<List<Proficiency>>();
		
		public List<List<Proficiency>> ProficienciesLearned { get; set; } = new List<List<Proficiency>>();
		
	}
}

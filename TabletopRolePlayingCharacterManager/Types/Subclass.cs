using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class Subclass
	{
		public string Name { get; set; } 
		public int NumOfSkillsProficiency { get; set; }

		public Dictionary<int, List<Skill>> SkillProficiencies { get; set; } = new Dictionary<int, List<Skill>>();
		public Dictionary<int, Tuple<int, List<Spell>>> SpellsLearned { get; set; } = new Dictionary<int, Tuple<int, List<Spell>>>();
		public Dictionary<int, List<Trait>> TraitsLearned { get; set; } = new Dictionary<int, List<Trait>>();
		public Dictionary<int, List<string>> LanguagesLearned { get; set; } = new Dictionary<int, List<string>>();
		
		public Dictionary<int, List<string>> ProficienciesLearned { get; set; } = new Dictionary<int, List<string>>();
		public Dictionary<int, List<string>> ArmorProficienciesLearned { get; set; } = new Dictionary<int, List<string>>();
		public Dictionary<int, List<string>> WeaponProficienciesLearned { get; set; } = new Dictionary<int, List<string>>();

		public bool EnablesMagic { get; set; }
		public int MagicStartLevel { get; set; }
	}
}

using System.Collections.Generic;
using System.Diagnostics;

namespace TabletopRolePlayingCharacterManager.Types
{
	public static class CharacterTraitSelectionStore
	{
		public static List<CharacterTrait> RacialTraits
		{
			get { return _racialTraits; }
		}

		public static List<CharacterTrait> ClassFeatures
		{
			get { return _classFeatures; }
		}

		public static List<string> Languages
		{
			get { return _languages; }
		}

		public static List<CharacterTrait> Proficiencies
		{
			get { return _proficiencies; }
		}

		public static List<string> CharacterArchetype
		{
			get { return _characterArchetype; }
		}

		private static List<string> races; 
		public static List<string> Races
		{
			get
			{
				if (races.Count == 0)
				{
					
				}
				return races;
			}
			private set { races = value; }
		}

		private static List<string> classes;
		private static List<CharacterTrait> _racialTraits = new List<CharacterTrait>();
		private static List<CharacterTrait> _classFeatures = new List<CharacterTrait>();
		private static List<string> _languages = new List<string>();
		private static List<CharacterTrait> _proficiencies = new List<CharacterTrait>();
		private static List<string> _characterArchetype = new List<string>();

		public static List<string> Classes
		{
			get
			{
				if (classes.Count == 0)
				{
					
				}
				return classes;
			}
			private set { classes = value; }
		}


		static CharacterTraitSelectionStore()
		{
			_racialTraits = new List<CharacterTrait>();
			_classFeatures = new List<CharacterTrait>();
			_languages = new List<string>();
			_proficiencies = new List<CharacterTrait>();
			_characterArchetype = new List<string>();
			Races = new List<string>();
			Classes = new List<string>();

			LoadClasses();
			LoadRaces();
			LoadLanguages();
		}

		public async static void LoadLanguages()
		{
		}

		public async static void LoadClasses()
		{
			
		}

		public async static void LoadRaces()
		{
			
		}
	}
}

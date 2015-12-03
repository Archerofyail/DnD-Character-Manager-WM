using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DnD_Character_Manager.Types
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
			string json = await JsonLoader.LoadJsonFromEmbeddedResource("LanguageList");
			Languages.AddRange(JsonConvert.DeserializeObject<string[]>(json));

		}

		public async static void LoadClasses()
		{
			string json = await JsonLoader.LoadJsonFromEmbeddedResource("ClassList");
			Debug.WriteLine("Got Json, deserializing...");
			var deseralizedJson = JsonConvert.DeserializeObject<string[]>(json);
			Debug.WriteLine("Deserialized json with value \n");
			foreach (var s in deseralizedJson)
			{
				Debug.WriteLine(s);
			}
			Classes.AddRange(deseralizedJson);
			Debug.WriteLine("Deserialized classes with value " + Classes);
		}

		public async static void LoadRaces()
		{
			string json = await JsonLoader.LoadJsonFromEmbeddedResource("RaceList");
			Races.AddRange(JsonConvert.DeserializeObject<string[]>(json));
		}
	}
}

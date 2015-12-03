using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace DnD_Character_Manager.Types
{
	public static class CharacterTraitSelectionStore
	{
		public static List<CharacterTrait> RacialTraits { get; private set; }
		public static List<CharacterTrait> ClassFeatures { get; private set; }
		public static List<CharacterTrait> Languages { get; private set; }
		public static List<CharacterTrait> Proficiencies { get; private set; }
		public static List<string> CharacterArchetype { get; private set; }

		private static List<string> races; 
		public static List<string> Races
		{
			get
			{
				if (races.Count == 0)
				{
					LoadRaces();
				}
				return races;
			}
			private set { races = value; }
		}

		private static List<string> classes; 
		public static List<string> Classes
		{
			get
			{
				if (classes.Count == 0)
				{
					LoadClasses();
				}
				return classes;
			}
			private set { classes = value; }
		}


		static CharacterTraitSelectionStore()
		{
			RacialTraits = new List<CharacterTrait>();
			ClassFeatures = new List<CharacterTrait>();
			Languages = new List<CharacterTrait>();
			Proficiencies = new List<CharacterTrait>();
			CharacterArchetype = new List<string>();
			Races = new List<string>();
			Classes = new List<string>();

			LoadClasses();
			LoadRaces();
			LoadLanguages();
		}

		public async static void LoadLanguages()
		{
			string json = await JsonLoader.LoadJsonFromEmbeddedResource("LanguageList.json");
			Languages = JsonConvert.DeserializeObject<List<CharacterTrait>>(json);

		}

		public async static void LoadClasses()
		{
			string json = await JsonLoader.LoadJsonFromEmbeddedResource("ClassList.json");
			Classes.AddRange(JsonConvert.DeserializeObject<List<string>>(json));
		}

		public async static void LoadRaces()
		{
			string json = await JsonLoader.LoadJsonFromEmbeddedResource("RaceList.json");
			Races.AddRange(JsonConvert.DeserializeObject<List<string>>(json));
		}
	}
}

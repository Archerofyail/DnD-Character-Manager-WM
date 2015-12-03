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


		static CharacterTraitSelectionStore()
		{
			RacialTraits = new List<CharacterTrait>();
			ClassFeatures = new List<CharacterTrait>();
			Languages = new List<CharacterTrait>();
			Proficiencies = new List<CharacterTrait>();
			CharacterArchetype = new List<string>();
		}

		public async static void LoadLanguages()
		{
			string json = await JsonLoader.LoadJsonFromFile("LanguagesList.json", ApplicationData.Current.RoamingFolder);
			Languages = JsonConvert.DeserializeObject<List<CharacterTrait>>(json);

		}
	}
}

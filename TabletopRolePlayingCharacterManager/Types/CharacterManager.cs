using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;
using Windows.Storage;


namespace TabletopRolePlayingCharacterManager.Types
{
	public static class CharacterManager
	{
		public static Character5E CurrentCharacter { get; set; }

		private static List<Character5E> characters = new List<Character5E>();

		public static List<Character5E> Characters
		{
			get { return characters; }
			private set { characters = value; }
		}

		public static Dictionary<string, RacialBonus> RacialBonuses { get; set; } = new Dictionary<string, RacialBonus>();
		public static Dictionary<string, ClassBonus> ClassBonuses { get; set; } = new Dictionary<string, ClassBonus>();
		public static List<Item> AllItems { get; set; } = new List<Item>();
		public static List<Spell> AllSpells { get; set; } = new List<Spell>();
		public static List<Weapon> AllWeapons { get; set; } = new List<Weapon>();

		private static StorageFolder SaveFolder { get; set; }
		private static StorageFolder RoamingFolder { get; set; }
		

		public static async Task SetFolders()
		{
			RoamingFolder = ApplicationData.Current.RoamingFolder;
			var savefolder = await ApplicationData.Current.RoamingFolder.TryGetItemAsync("Characters");
			if (savefolder != null)
			{

				SaveFolder = await ApplicationData.Current.RoamingFolder.GetFolderAsync("Characters");

			}
			else
			{
				SaveFolder = await ApplicationData.Current.RoamingFolder.CreateFolderAsync("Characters");
			}
			Debug.WriteLine("Roaming Folder is at: " + SaveFolder.Path);
		}

		public static async Task LoadAllCharacters()
		{
			if (SaveFolder == null)
			{
				await SetFolders();
			}
			foreach (var file in await SaveFolder.GetFilesAsync())
			{
				if (file.FileType.ToLower().Contains("json"))
				{
					characters.Add(JsonConvert.DeserializeObject<Character5E>(await FileIO.ReadTextAsync(file)));
				}
			}
		}

		internal async static void SaveCurrentCharacter()
		{
			var saveName = CurrentCharacter.id + ".json";
			var json = JsonConvert.SerializeObject(CurrentCharacter);
			var potsave = await SaveFolder.TryGetItemAsync(saveName);
			StorageFile saveFile;
			if (potsave != null)
			{
				saveFile = await SaveFolder.GetFileAsync(saveName);
				await FileIO.WriteTextAsync(saveFile, json);

			}
			else
			{
				saveFile = await SaveFolder.CreateFileAsync(saveName);
				await FileIO.WriteTextAsync(saveFile, json);
			}

		}

		public async static void LoadRaces()
		{
			var fileName = "Races.json";
			var file = await GetOrCreateStorageFile(fileName);
			RacialBonuses = LoadObjectFromJsonFile<Dictionary<string, RacialBonus>>(file);



		}

		public async static void SaveRaces()
		{

		}

		public async static void LoadItems()
		{
			var fileName = "Items.json";
			var file = await GetOrCreateStorageFile(fileName);
			AllItems = LoadObjectFromJsonFile<List<Item>>(file);
		}

		public async static void SaveItems()
		{

		}

		public async static void LoadClasses()
		{
			var fileName = "Classes.json";
			var file = await GetOrCreateStorageFile(fileName);
			ClassBonuses = LoadObjectFromJsonFile<Dictionary<string, ClassBonus>>(file);
		}

		public async static void SaveClasses()
		{

		}

		public async static void LoadSpells()
		{
			var fileName = "Spells.json";
			var file = await GetOrCreateStorageFile(fileName);
			AllSpells = LoadObjectFromJsonFile<List<Spell>>(file);
		}

		public async static void SaveSpells()
		{

		}

		public async static void LoadWeapons()
		{
			var fileName = "Weapons.json";

			var file = await GetOrCreateStorageFile(fileName);
			AllWeapons = LoadObjectFromJsonFile<List<Weapon>>(file);


		}



		public async static void SaveWeapons()
		{

		}

		public async static Task<StorageFile> GetOrCreateStorageFile(string fileName)
		{
			StorageFile file;
			var result = await RoamingFolder.TryGetItemAsync(fileName);
			if (result != null)
			{
				file = await RoamingFolder.GetFileAsync(fileName);
			}
			else
			{
				file = await RoamingFolder.CreateFileAsync(fileName);
			}
			return file;
		}

		private static async void SaveObjectToFile(object saveObject, StorageFile file)
		{
			var json = JsonConvert.SerializeObject(saveObject);
			await FileIO.WriteTextAsync(file, json);
		}

		private static T LoadObjectFromJsonFile<T>(StorageFile file)
		{
			T temp;
			var json = FileIO.ReadTextAsync(file).GetResults();
			temp = JsonConvert.DeserializeObject<T>(json);
			return temp;
		}

		public static
			Character5E GetNewChar()
		{
			var character = new Character5E(Characters.Count);
			Characters.Add(character);
			CurrentCharacter = character;
			return character;
		}

	}

}

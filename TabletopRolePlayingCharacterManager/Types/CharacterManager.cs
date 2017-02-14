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
			get
			{
				if (characters.Count == 0)
				{  }
				return characters;
			}
			private set { characters = value; }
		}

		public static Dictionary<string, RacialBonus> RacialBonuses { get; set; } = new Dictionary<string, RacialBonus>();
		public static Dictionary<string, ClassBonus> ClassBonuses { get; set; } = new Dictionary<string, ClassBonus>();
		public static List<Item> AllItems { get; set; } = new List<Item>();
		public static List<Spell> AllSpells { get; set; } = new List<Spell>();
		public static List<Weapon> AllWeapons { get; set; } = new List<Weapon>();
		public static List<string> AllLanguages { get; set; } = new List<string>();
		private static StorageFolder SaveFolder { get; set; }
		private static StorageFolder RoamingFolder { get; set; }

		public static EventHandler CharactersLoadedEventHandler;


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
			if (Characters.Count == 0)
			{
				foreach (var file in await SaveFolder.GetFilesAsync())
				{
					if (file.FileType.ToLower().Contains("json"))
					{
						var json = await FileIO.ReadTextAsync(file);
						Debug.WriteLine("Json for file is " + json);
						var character = JsonConvert.DeserializeObject<Character5E>(json);
						character.CalculateAbilityModifiers();
						character.CalculateSkillBonuses();
						characters.Add(character);
					}
				}
			}
			CharactersLoadedEventHandler(typeof(CharacterManager), EventArgs.Empty);
		}

		internal async static Task SaveCurrentCharacter()
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

		public async static Task LoadRaces()
		{
			var fileName = "Races.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				RacialBonuses = await LoadObjectFromJsonFile<Dictionary<string, RacialBonus>>(file);
			}
		}

		public async static Task SaveRaces()
		{
			var fileName = "Races.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(RacialBonuses, file);
			}
		}

		public async static Task LoadItems()
		{
			var fileName = "Items.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllItems = await LoadObjectFromJsonFile<List<Item>>(file);
			}
		}

		public async static Task SaveItems()
		{
			var fileName = "Items.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(AllItems, file);
			}
		}

		public async static Task LoadClasses()
		{
			var fileName = "Classes.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				ClassBonuses = await LoadObjectFromJsonFile<Dictionary<string, ClassBonus>>(file);
			}
		}

		public async static Task SaveClasses()
		{

			var fileName = "Classes.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(ClassBonuses, file);
			}
		}

		public async static Task LoadSpells()
		{
			var fileName = "Spells.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllSpells = await LoadObjectFromJsonFile<List<Spell>>(file);
			}
		}

		public async static Task SaveSpells()
		{
			var fileName = "Spells.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(AllSpells, file);
			}
		}

		public async static Task LoadWeapons()
		{
			var fileName = "Weapons.json";

			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllWeapons = await LoadObjectFromJsonFile<List<Weapon>>(file);
			}
		}

		public async static Task SaveWeapons()
		{
			var fileName = "Weapons.json";

			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(AllWeapons, file);
			}
		}

		public async static Task LoadLanguagues()
		{
			var fileName = "Languages.json";

			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllLanguages = await LoadObjectFromJsonFile<List<string>>(file);
				if (AllLanguages.Count == 0)
				{
					AllLanguages.Add("Common");
					AllLanguages.Add("Elvish");
					AllLanguages.Add("Dwarvish");
					AllLanguages.Add("Goblin");
				}
			}
		}

		public static async Task LoadCompendium()
		{
			await LoadAllCharacters();
			await LoadRaces();
			await LoadClasses();
			await LoadItems();
			await LoadWeapons();
			await LoadSpells();

		}

		public static async void SaveAll()
		{
			await SaveRaces();
			await SaveClasses();
			await SaveItems();
			await SaveWeapons();
			await SaveSpells();
		}

		public async static Task<StorageFile> GetOrCreateStorageFile(string fileName)
		{
			StorageFile file;
			try
			{
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
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
				throw;
			}

		}

		private static async Task SaveObjectToFile(object saveObject, StorageFile file)
		{
			var json = JsonConvert.SerializeObject(saveObject);
			await FileIO.WriteTextAsync(file, json);
		}

		private async static Task<T> LoadObjectFromJsonFile<T>(StorageFile file)
		{
			T temp;
			var json = await FileIO.ReadTextAsync(file);
			temp = JsonConvert.DeserializeObject<T>(json);
			return temp;
		}

		public static
			Character5E GetNewChar()
		{
			var character = new Character5E(Characters.Count, true);
			Characters.Add(character);
			CurrentCharacter = character;
			return character;
		}

	}

}

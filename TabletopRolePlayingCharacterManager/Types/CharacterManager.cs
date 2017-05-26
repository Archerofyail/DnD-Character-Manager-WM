using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;
using Windows.Storage;


namespace TabletopRolePlayingCharacterManager.Types
{
	public static class CharacterManager
	{
		public static Character5E CurrentCharacter { get; set; }

		private static List<Character5E> _characters = new List<Character5E>();

		public static List<Character5E> Characters
		{
			get
			{
				if (_characters.Count == 0)
				{ }
				return _characters;
			}
			private set => _characters = value;
		}

		public static List<RacialBonus> RacialBonuses { get; set; } = new List<RacialBonus>();
		public static List<ClassBonus> ClassBonuses { get; set; } = new List<ClassBonus>();
		public static List<Item> AllItems { get; set; } = new List<Item>();
		public static List<Spell> AllSpells { get; set; } = new List<Spell>();
		public static List<Weapon> AllWeapons { get; set; } = new List<Weapon>();
		public static List<string> AllLanguages { get; set; } = new List<string>();
		public static List<Skill> AllSkills { get; set; } = new List<Skill>
				{
					new Skill("Acrobatics", 1, 1, MainStatType.Dexterity, false),
					new Skill("Animal Handling", 1, 1, MainStatType.Wisdom, false),
					new Skill("Arcana", 1, 1, MainStatType.Intelligence, false),
					new Skill("Athletics", 1 , 1, MainStatType.Strength, false),
					new Skill("Deception", 1 , 1, MainStatType.Charisma, false),
					new Skill("History", 1 , 1, MainStatType.Intelligence, false),
					new Skill("Insight", 1 , 1, MainStatType.Wisdom, false),
					new Skill("Intimidation", 1 , 1, MainStatType.Charisma, false),
					new Skill("Investigation", 1 , 1, MainStatType.Intelligence, false),
					new Skill("Medicine", 1 , 1, MainStatType.Wisdom, false),
					new Skill("Nature", 1 , 1, MainStatType.Intelligence, false),
					new Skill("Perception", 1 , 1, MainStatType.Wisdom, false),
					new Skill("Performance", 1 , 1, MainStatType.Charisma, false),
					new Skill("Persuasion", 1 , 1, MainStatType.Charisma, false),
					new Skill("Religion", 1 , 1, MainStatType.Intelligence, false),
					new Skill("Sleight of Hand", 1 , 1, MainStatType.Dexterity, false),
					new Skill("Stealth", 1 , 1, MainStatType.Dexterity, false),
					new Skill("Survival", 1 , 1, MainStatType.Wisdom, false),
				};
		private static List<StatIncrease> _statBonuses = new List<StatIncrease>();
		public static List<StatIncrease> StatBonuses
		{
			get
			{
				if (_statBonuses.Count == 0)
				{
					var statIncType = typeof(StatIncrease);
					var allStatIncTypes = statIncType.GetTypeInfo()
						.Assembly.GetTypes()
						.Where((type) => type.GetTypeInfo().IsClass && type.GetTypeInfo().IsSubclassOf(statIncType));
					foreach (var statType in allStatIncTypes)
					{

						_statBonuses.Add((StatIncrease)Activator.CreateInstance(statType));

					}
				}
				return _statBonuses;
			}
		}
		private static StorageFolder SaveFolder { get; set; }
		private static StorageFolder RoamingFolder { get; set; }

		public static EventHandler CharactersLoadedEventHandler;


		static CharacterManager()
		{

		}

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
						_characters.Add(character);
					}
				}
			}
			CharactersLoadedEventHandler(typeof(CharacterManager), EventArgs.Empty);
		}

		internal static async Task SaveCurrentCharacter()
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

		public static async Task LoadRaces()
		{
			var fileName = "Races.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				RacialBonuses.AddRange(await LoadObjectFromJsonFile<List<RacialBonus>>(file) ?? new List<RacialBonus>());
			}
		}

		public static async Task SaveRaces()
		{
			var fileName = "Races.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(RacialBonuses, file);
			}
		}

		public static async Task LoadItems()
		{
			var fileName = "Items.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllItems.AddRange(await LoadObjectFromJsonFile<List<Item>>(file));
			}
		}

		public static async Task SaveItems()
		{
			var fileName = "Items.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(AllItems, file);
			}
		}

		public static async Task LoadClasses()
		{
			var fileName = "Classes.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				ClassBonuses.AddRange(await LoadObjectFromJsonFile<List<ClassBonus>>(file));
			}
		}

		public static async Task SaveClasses()
		{

			var fileName = "Classes.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(ClassBonuses, file);
			}
		}

		public static async Task LoadSpells()
		{
			var fileName = "Spells.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllSpells.AddRange(await LoadObjectFromJsonFile<List<Spell>>(file));
			}
		}

		public static async Task SaveSpells()
		{
			var fileName = "Spells.json";
			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(AllSpells, file);
			}
		}

		public static async Task LoadWeapons()
		{
			var fileName = "Weapons.json";

			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllWeapons.AddRange(await LoadObjectFromJsonFile<List<Weapon>>(file));
			}
		}

		public static async Task SaveWeapons()
		{
			var fileName = "Weapons.json";

			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				await SaveObjectToFile(AllWeapons, file);
			}
		}

		public static async Task LoadLanguagues()
		{
			var fileName = "Languages.json";

			var file = await GetOrCreateStorageFile(fileName);
			if (file != null)
			{
				AllLanguages.AddRange(await LoadObjectFromJsonFile<List<string>>(file));
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
			AppendToLists();
		}

		public static async void SaveAll()
		{
			await SaveRaces();
			await SaveClasses();
			await SaveItems();
			await SaveWeapons();
			await SaveSpells();
		}

		public static async Task<StorageFile> GetOrCreateStorageFile(string fileName)
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

		private static async Task<T> LoadObjectFromJsonFile<T>(StorageFile file) where T : class, new()
		{
			T temp;
			var json = await FileIO.ReadTextAsync(file);
			temp = JsonConvert.DeserializeObject<T>(json);
			return temp ?? new T();
		}

		public static Character5E GetNewChar()
		{
			var character = new Character5E(Characters.Count, true);
			Characters.Add(character);
			CurrentCharacter = character;
			return character;
		}

		public static StatIncrease GetStatIncrease(string name)
		{
			StatIncrease statInc;

			var statIncType = StatBonuses.Find((increase => increase.BonusName == name)).GetType();
			statInc = (StatIncrease)Activator.CreateInstance(statIncType);

			return statInc;
		}

		public static void AppendToLists()
		{
			RacialBonuses.Add(new RacialBonus
			{
				Race = "Dwarf",
				StatBonuses = new List<List<AbilityScoreBonusModel>>
				{
					new List<AbilityScoreBonusModel>
					{
						new AbilityScoreBonusModel{Bonus = 2, Stat = MainStatType.Constitution}
					}
				},

				Proficiencies = new List<List<Proficiency>>
				{
					new List<Proficiency>
					{
						new Proficiency(ProficiencyType.Weapon, "Battleaxe")
					},
					new List<Proficiency> {new Proficiency(ProficiencyType.Weapon, "Warhammer") },
					new List<Proficiency> {new Proficiency(ProficiencyType.Weapon, "Throwing Hammer")},
					new List<Proficiency> {new Proficiency(ProficiencyType.Weapon, "Handaxe")},
					new List<Proficiency> {new Proficiency(ProficiencyType.Tool, "Smith's Tools"), new Proficiency(ProficiencyType.Tool, "Brewer's Supplies"), new Proficiency(ProficiencyType.Tool, "Mason's Tools") },
					new List<Proficiency> {new Proficiency(ProficiencyType.Language, "Dwarvish")},
					new List<Proficiency> {new Proficiency(ProficiencyType.Language, "Common")}
				},
				Traits = new List<List<Trait>>
				{
					new List<Trait>
					{
						new Trait
						{
							Description = "Darkvision - You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can’t discern color in darkness, only shades of gray."
						}
					},
					new List<Trait>
					{
						new Trait{ Description = "Dwarven Resilience - You have advantage on saving throws against poison, and you have resistance against poison damage (explained in chapter 9)."}
					},
					new List<Trait>
					{
						new Trait {Description = "Stonecunning. Whenever you make an Intelligence (History) check related to the origin of stonework, you are considered proficient in the History skill and add double your proficiency bonus to the check, instead of your normal proficiency bonus."}
					}
				}
			});
			RacialBonuses.Add(new RacialBonus
			{
				Race = "Hill Dwarf",
				ParentRace = "Dwarf",
				StatBonuses = new List<List<AbilityScoreBonusModel>>
				{
					new List<AbilityScoreBonusModel>
					{
						new AbilityScoreBonusModel{Bonus = 1, Stat = MainStatType.Wisdom}
					}
				},
				Traits = new List<List<Trait>>()
				{
					new List<Trait>{new Trait { Description = "Dwarven Toughness. Your hit point maximum increases by 1, and it increases by 1 every time you gain a level." } }
				}
			});

			RacialBonuses.Add(new RacialBonus
			{
				Race = "Mountain Dwarf",
				ParentRace = "Dwarf",
				StatBonuses = new List<List<AbilityScoreBonusModel>>
				{
					new List<AbilityScoreBonusModel>
					{
						new AbilityScoreBonusModel{Bonus = 2, Stat = MainStatType.Strength}
					}
				},
				Proficiencies = new List<List<Proficiency>>
				{
					new List<Proficiency>()
					{
						new Proficiency(ProficiencyType.Armor, "Light Armor"),

					},
					new List<Proficiency>()
					{
						new Proficiency(ProficiencyType.Armor, "Medium Armor")
					}
				}
			});
		}

	}

}

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using SQLite.Net;
using TabletopRolePlayingCharacterManager.Models;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System.Collections.Generic;
using SQLiteNetExtensionsAsync.Extensions;
using TabletopRolePlayingCharacterManager.Models.Intermediates;

namespace TabletopRolePlayingCharacterManager.Types
{
	public static class DbLoader
	{
		public static ResourceLoader ResourceLoader;
		public static SQLiteAsyncConnection DbConnection;
		private static string _dbPath = "Database.sqlite";

		public static List<Race> Races
		{
			get
			{
				var races = DbConnection.GetAllWithChildrenAsync<Race>().Result;
				return races;

			}
		}

		public static List<Subrace> Subraces
		{
			get
			{
				return DbConnection.GetAllWithChildrenAsync<Subrace>().Result;
			}
		}

		public static List<Class> Classes
		{
			get
			{
				return DbConnection.GetAllWithChildrenAsync<Class>().Result;
			}
		}
		public static List<Subclass> Subclasses
		{
			get
			{
				return DbConnection.GetAllWithChildrenAsync<Subclass>().Result;
			}
		}

		public static List<Character5E> Characters
		{
			get
			{
				return DbConnection.GetAllWithChildrenAsync<Character5E>().Result;
			}
		}

		public static List<Skill> Skills
		{
			get { return DbConnection.GetAllWithChildrenAsync<Skill>().Result; }
		}

		public static List<Alignment> Alignments
		{
			get
			{
				return DbConnection.GetAllWithChildrenAsync<Alignment>().Result;
			}
		}

		static DbLoader()
		{

			Debug.WriteLine("db created ");
		}

		public static async void CreateData(bool deleteAll = false)
		{
			DbConnection = new SQLiteAsyncConnection(ConnectToDb);
			bool isFirstRun = true;
			if (deleteAll)
			{
				Debug.WriteLine("Deleting All data");
				await DeleteAllData();
			}

			Debug.WriteLine("Creating Tables");
			//Table Creation
			await DbConnection.CreateTableAsync<Skill>();
			await DbConnection.CreateTableAsync<Race>();
			await DbConnection.CreateTableAsync<Alignment>();
			await DbConnection.CreateTableAsync<Proficiency>();
			await DbConnection.CreateTableAsync<Class>();
			await DbConnection.CreateTableAsync<Item>();
			await DbConnection.CreateTableAsync<Spell>();
			await DbConnection.CreateTableAsync<Subclass>();
			await DbConnection.CreateTableAsync<Subrace>();
			await DbConnection.CreateTableAsync<Weapon>();

			await DbConnection.CreateTableAsync<Character5E>();
			await DbConnection.CreateTableAsync<CharacterPreparedSpells>();
			await DbConnection.CreateTableAsync<CharacterSkillProficiency>();

			//Intermediate Tables for Many to Many relations
			await DbConnection.CreateTableAsync<CharacterArmor>();
			await DbConnection.CreateTableAsync<CharacterItem>();
			await DbConnection.CreateTableAsync<CharacterProficiency>();
			await DbConnection.CreateTableAsync<CharacterSkill>();
			await DbConnection.CreateTableAsync<CharacterSpell>();
			await DbConnection.CreateTableAsync<CharacterWeapon>();




			await DbConnection.CreateTableAsync<FirstRun>();
			if ((await DbConnection.Table<FirstRun>().ToListAsync()).Count == 0)
			{

				Debug.WriteLine("Recreating default data");
				await DbConnection.InsertAsync(new FirstRun { IsFirstRun = isFirstRun });


				//default data skills
				await DbConnection.InsertAsync(new Skill("Deception", MainStat.Dexterity));


				//Default data Races
				var race1 = new Race("Human", "Sturdy Creatures");
				var race2 = new Race("Dwarves", "Short, Strong, live in mountains or hills usually");
				var race3 = new Race("Elf", "Live forever");
				await DbConnection.InsertAsync(race1);
				await DbConnection.InsertAsync(race2);
				await DbConnection.InsertAsync(race3);

				//Default data Proficiencies
				await DbConnection.InsertAsync(new Proficiency
				{
					Name = "Common",
					Description = "The english language",
					Category = "Language"

				});
				await DbConnection.InsertAsync(new Proficiency
				{
					Name = "Short Sword",
					Description = "Proficiency with a short sword",
					Category = "Weapon"

				});
				await DbConnection.InsertAsync(new Proficiency
				{
					Name = "Light Armor",
					Description = "Proficiency with light armor such as leather, or hide",
					Category = "Armor"

				});

				//Default data Alignments
				var alignmentList = new List<Alignment>
				{
					new Alignment
					{
						Name = "True Neutral"
					},
					new Alignment
					{
						Name="Chaotic Neutral"
					},
					new Alignment
					{
						Name="Lawful Neutral"
					},
					new Alignment
					{
						Name="Lawful Good"
					},
					new Alignment
					{
						Name="Chaotic Good"
					},
					new Alignment
					{
						Name="Neutral Good"
					},
					new Alignment
					{
						Name="Neutral Evil"
					},
					new Alignment
					{
						Name="Lawful Evil"
					},
					new Alignment
					{
						Name="Chaotic Evil"
					},
				};
				await DbConnection.InsertAllAsync(alignmentList);




				//Default data Class
				var class1 = new Class
				{
					Name = "Wizard",
					Description = "Magic users of insane power",
					InitHp = 6,
					HitDieSize = 6,
					HpPerLevel = "d6"
				};
				var class2 = new Class
				{
					Name = "Warrior",
					Description = "Super strong",
					InitHp = 10,
					HitDieSize = 10,
					HpPerLevel = "d10"
				};
				await DbConnection.InsertAsync(class1);
				await DbConnection.InsertAsync(class2);


				//Default data Items
				await DbConnection.InsertAsync(new Item
				{
					Name = "Healing Potion",
					Description = "Heals 2d4+2"
				});


				await DbConnection.InsertAsync(new Spell
				{
					Name = "Eldritch Blat",
					Description = "Fires a blat of energy in a shape that you can choose",
					Damage = "1d10",

				});



				await DbConnection.InsertAsync(new Subclass
				{
					Name = "Abjuration",
					Description = "The School of Abjuration emphasizes magic that blocks,banishes, or protects. Detractors o f this school say that its tradition is about denial, negation rather than positive assertion. You understand, however, that ending harmful effects, protecting the weak, and banishing evil influences is anything but a philosophical void. It is a proud and respected vocation.",
					ParentClass = class1
				});

				await DbConnection.InsertAsync(new Subclass
				{
					Name = "Conjuration",
					Description = "The School of conjuration emphasizes magic that blocks,banishes, or protects. Detractors o f this school say that its tradition is about denial, negation rather than positive assertion. You understand, however, that ending harmful effects, protecting the weak, and banishing evil influences is anything but a philosophical void. It is a proud and respected vocation.",
					ParentClass = class1

				});


				await DbConnection.InsertAsync(new Subrace
				{
					Name = "Hill Dwarves",
					Description = "Live in hills, +1 dexterity",
					ParentRace = race2
				});

				await DbConnection.InsertAsync(new Subrace
				{
					Name = "Northern Human",
					Description = "Live in the north, + resistance to cold",
					ParentRace = race1
				});



				await DbConnection.InsertAsync(new Weapon
				{
					Name = "Shortsword",
					Description = "A short sword made of steel, fairly sturdy",
					AttackBonus = 0,
					DamageDie = 6,
					DamageBonus = 2,
				});
				//Set up relationships





			}

		}

		public static SQLiteConnectionWithLock ConnectToDb()
		{
			SQLiteConnectionWithLock dbConn;
			try
			{
				dbConn = new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(Path.Combine(ApplicationData.Current.RoamingFolder.Path, _dbPath), false));
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Failed to open database with error:" + ex.Message);
				dbConn = new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(_dbPath, false));
			}
			return dbConn;
		}

		public static async Task DeleteAllData()
		{
			await DbConnection.DropTableAsync<FirstRun>();
			await DbConnection.DropTableAsync<Skill>();
			await DbConnection.DropTableAsync<Race>();
			await DbConnection.DropTableAsync<Proficiency>();
			await DbConnection.DropTableAsync<Alignment>();
			await DbConnection.DropTableAsync<Class>();
			await DbConnection.DropTableAsync<Item>();
			await DbConnection.DropTableAsync<Spell>();
			await DbConnection.DropTableAsync<Subclass>();
			await DbConnection.DropTableAsync<Subrace>();
			await DbConnection.DropTableAsync<Weapon>();
			await DbConnection.DropTableAsync<Character5E>();
			await DbConnection.DropTableAsync<CharacterPreparedSpells>();
			await DbConnection.DropTableAsync<CharacterSkillProficiency>();
			//Intermediate Tables for Many to Many relations
			await DbConnection.DropTableAsync<CharacterArmor>();
			await DbConnection.DropTableAsync<CharacterItem>();
			await DbConnection.DropTableAsync<CharacterProficiency>();
			await DbConnection.DropTableAsync<CharacterSkill>();
			await DbConnection.DropTableAsync<CharacterSpell>();
			await DbConnection.DropTableAsync<CharacterWeapon>();
		}

		public static List<T> GetTable<T>() where T : class
		{
			try
			{
				Debug.WriteLine("Attempting to get a list of " + typeof(T).ToString() + " from the db.");
				var query = DbConnection.Table<T>();
				return query.ToListAsync().Result;
			}
			catch (AggregateException ex)
			{
				Debug.WriteLine("Failed to get data from the database with error: " + ex.Message +", returning empty list");
				return new List<T>();
			}

		}
	}
}

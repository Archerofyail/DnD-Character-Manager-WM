using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using SQLite.Net;
using TabletopRolePlayingCharacterManager.Models;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	public static class DBLoader
	{
		public static ResourceLoader resourceLoader;
		public static SQLiteAsyncConnection dbConnection;
		private static string DBPath = "Database.sqlite";

		static DBLoader()
		{
			
			Debug.WriteLine("db created ");
		}

		public static async void CreateData(bool DeleteAll = false)
		{
			dbConnection = new SQLiteAsyncConnection(ConnectToDB);
			bool isFirstRun = true;
			if (DeleteAll)
			{
				Debug.WriteLine("Deleting All data");
				await DeleteAllData();
			}

			Debug.WriteLine("Creating Tables");
			//Table Creation
			await dbConnection.CreateTableAsync<Skill>();
			await dbConnection.CreateTableAsync<Race>();
			await dbConnection.CreateTableAsync<Alignment>();
			await dbConnection.CreateTableAsync<Proficiency>();
			await dbConnection.CreateTableAsync<Class>();
			await dbConnection.CreateTableAsync<Item>();
			await dbConnection.CreateTableAsync<Spell>();
			await dbConnection.CreateTableAsync<Subclass>();
			await dbConnection.CreateTableAsync<Subrace>();
			await dbConnection.CreateTableAsync<Weapon>();

			await dbConnection.CreateTableAsync<Character5E>();
			await dbConnection.CreateTableAsync<CharacterPreparedSpells>();
			await dbConnection.CreateTableAsync<CharacterSkillProficiency>();

			//Intermediate Tables for Many to Many relations
			await dbConnection.CreateTableAsync<CharacterArmor>();
			await dbConnection.CreateTableAsync<CharacterItem>();
			await dbConnection.CreateTableAsync<CharacterProficiency>();
			await dbConnection.CreateTableAsync<CharacterRace>();
			await dbConnection.CreateTableAsync<CharacterSkill>();
			await dbConnection.CreateTableAsync<CharacterSpell>();
			await dbConnection.CreateTableAsync<CharacterWeapon>();



			await dbConnection.CreateTableAsync<FirstRun>();
			if ( (await dbConnection.Table<FirstRun>().ToListAsync()).Count == 0)
			{

				Debug.WriteLine("Recreating default data");
				await dbConnection.InsertAsync(new FirstRun());

				
				//default data skills
				await dbConnection.InsertAsync(new Skill("Deception", MainStat.Dexterity));

				
				//Default data Races
				await dbConnection.InsertAsync(new Race("Human", "Sturdy Creatures"));
				await dbConnection.InsertAsync(new Race("Dwarves", "Short, Strong, live in mountains or hills usually"));
			
				//Default data Proficiencies
				await dbConnection.InsertAsync(new Proficiency
				{
					Name = "Common",
					Description = "The english language",
					Category = "Language"

				});
				await dbConnection.InsertAsync(new Proficiency
				{
					Name = "Short Sword",
					Description = "Proficiency with a short sword",
					Category = "Weapon"

				});
				await dbConnection.InsertAsync(new Proficiency
				{
					Name = "Light Armor",
					Description = "Proficiency with light armor such as leather, or hide",
					Category = "Armor"

				});
				
				//Default data Alignments
				await dbConnection.InsertAsync(new Alignment
				{
					Name = "True Neutral"
				});



				
				//Default data Class
				await dbConnection.InsertAsync(new Class
				{
					Name = "Wizard",
					Description = "Magic users of insane power",
					InitHP = 6,
					HitDieSize = 6,
					HPPerLevel = "d6"
				});

				
				//Default data Items
				await dbConnection.InsertAsync(new Item
				{
					Name = "Healing Potion",
					Description = "Heals 2d4+2"
				});

				
				await dbConnection.InsertAsync(new Spell
				{
					Name = "Eldritch Blat",
					Description = "Fires a blat of energy in a shape that you can choose",
					Damage = "1d10",

				});


				
				await dbConnection.InsertAsync(new Subclass
				{
					Name = "Abjuration",
					Description = "The School of Abjuration emphasizes magic that blocks,banishes, or protects. Detractors o f this school say that its tradition is about denial, negation rather than positive assertion. You understand, however, that ending harmful effects, protecting the weak, and banishing evil influences is anything but a philosophical void. It is a proud and respected vocation."

				});

				
				await dbConnection.InsertAsync(new Subrace
				{
					Name = "Hill Dwarves",
					Description = "Live in hills, +1 dexterity"
				});


				
				await dbConnection.InsertAsync(new Weapon
				{
					Name = "Shortsword",
					Description = "A short sword made of steel, fairly sturdy",
					AttackBonus = 0,
					DamageDie = 6,
					DamageBonus = 2,
				});

				


				
			}

		}

		public static SQLiteConnectionWithLock ConnectToDB()
		{
			SQLiteConnectionWithLock dbConn;
			try
			{
				dbConn = new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(Path.Combine(ApplicationData.Current.RoamingFolder.Path, DBPath), false));
			}
			catch (Exception ex)
			{

				dbConn = new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(DBPath, false));
			}
			return dbConn;
		}

		public static async 
		Task
DeleteAllData()
		{
			await dbConnection.DropTableAsync<FirstRun>();
			await dbConnection.DropTableAsync<Skill>();
			await dbConnection.DropTableAsync<Race>();
			await dbConnection.DropTableAsync<Proficiency>();
			await dbConnection.DropTableAsync<Alignment>();
			await dbConnection.DropTableAsync<Class>();
			await dbConnection.DropTableAsync<Item>();
			await dbConnection.DropTableAsync<Spell>();
			await dbConnection.DropTableAsync<Subclass>();
			await dbConnection.DropTableAsync<Subrace>();
			await dbConnection.DropTableAsync<Weapon>();
			await dbConnection.DropTableAsync<Character5E>();
			await dbConnection.DropTableAsync<CharacterPreparedSpells>();
			await dbConnection.DropTableAsync<CharacterSkillProficiency>();
			//Intermediate Tables for Many to Many relations
			await dbConnection.DropTableAsync<CharacterArmor>();
			await dbConnection.DropTableAsync<CharacterItem>();
			await dbConnection.DropTableAsync<CharacterProficiency>();
			await dbConnection.DropTableAsync<CharacterRace>();
			await dbConnection.DropTableAsync<CharacterSkill>();
			await dbConnection.DropTableAsync<CharacterSpell>();
			await dbConnection.DropTableAsync<CharacterWeapon>();
		}

		public static List<T> GetTableFromDB<T>() where T : class
		{
			try
			{
				Debug.WriteLine("Attempting to get a list of " + typeof(T).ToString() + " from the db.");
				var query = dbConnection.Table<T>();
				return query.ToListAsync().Result;
			}
			catch (AggregateException ex)
			{
				Debug.WriteLine("Failed to get data from the database, returning empty list");
				return new List<T>();
			}

		}
	}
}

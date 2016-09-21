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


		static DBLoader()
		{
			CreatePreexistingData();
			Debug.WriteLine("db created ");
		}

		public static async void CreatePreexistingData()
		{

			dbConnection = new SQLiteAsyncConnection(ConnectToDB);
			
			await dbConnection.CreateTableAsync<Skill>();
			//default data skills
			await dbConnection.InsertAsync(new Skill("Deception", MainStat.Dexterity));

			await dbConnection.CreateTableAsync<Race>();
			//Default data Races
			await dbConnection.InsertAsync(new Race("Human", "Sturdy Creatures"));
			await dbConnection.InsertAsync(new Race("Dwarves", "Short, Strong, live in mountains or hills usually"));
			await dbConnection.CreateTableAsync<Proficiency>();
			//Default data Proficiencies
			await dbConnection.InsertAsync(new Proficiency {
				Name = "Common",
				Description = "The english language",
				Category = "Language"
			});
			await dbConnection.CreateTableAsync<Alignment>();
			//Default data Alignments
			await dbConnection.InsertAsync(new Alignment
			{
				alignment = "True Neutral"
			});
			

			
			await dbConnection.CreateTableAsync<Class>();
			//Default data Class
			await dbConnection.InsertAsync(new Class("Wizard", "Magic users of insane power"));

			await dbConnection.CreateTableAsync<Item>();
			//Default data Items
			await dbConnection.InsertAsync(new Item
			{
				Name = "Healing Potion",
				Description = "Heals 2d4+2"
			});			

			await dbConnection.CreateTableAsync<Spell>();
			await dbConnection.InsertAsync(new Spell
			{
				Name = "Eldritch Blat",
				Description = "Fires a blat of energy in a shape that you can choose",
				Damage = "1d10",

			});


			await dbConnection.CreateTableAsync<Subclass>();
			await dbConnection.InsertAsync(new Subclass
			{
				Name = "Abjuration",
				Description = "The School of Abjuration emphasizes magic that blocks,banishes, or protects. Detractors o f this school say that its tradition is about denial, negation rather than positive assertion. You understand, however, that ending harmful effects, protecting the weak, and banishing evil influences is anything but a philosophical void. It is a proud and respected vocation."
				
			});

			await dbConnection.CreateTableAsync<Subrace>();
			await dbConnection.InsertAsync(new Subrace
			{
				Name = "Hill Dwarves",
				Description = "Live in hills, +1 dexterity"
			});


			await dbConnection.CreateTableAsync<Weapon>();
			await dbConnection.InsertAsync(new Weapon
			{
				Name = "Shortsword",
				Description = "A short sword made of steel, fairly sturdy",
				AttackBonus = 0,
				DamageDie = 6,
				DamageBonus = 2,
			});

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


		}

		public static SQLiteConnectionWithLock ConnectToDB()
		{
			SQLiteConnectionWithLock dbConn = new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString("Database", false));
			return dbConn;
		}

		public static List<T> GetTableFromDB<T>() where T : class
		{
			var query = dbConnection.Table<T>();
			return query.ToListAsync().Result;

		}
	}
}

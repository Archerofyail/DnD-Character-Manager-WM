using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;

namespace TabletopRolePlayingCharacterManager.Types
{
	public static class ItemSelectionStore
	{
		public static List<Armor> armor { get;private set; }
		public static List<Weapon> weapons { get; private set; }

		public static List<string> armorProfList { get; private set; } 
		public static List<string> weaponProfList { get; set; } 

		static ItemSelectionStore()
		{
			armor = new List<Armor>();
			weapons = new List<Weapon>();
			armorProfList = new List<string>();
			weaponProfList = new List<string>();
			//LoadArmorList();
			//LoadWeaponList();
			LoadProficiencies();
		}

		public async static void LoadProficiencies()
		{
			
			string weaponJson = "";
			string armorJson = "";
			//weaponJson = await JsonLoader.LoadJsonFromEmbeddedResource("WeaponProficienciesList");
			//armorJson = await JsonLoader.LoadJsonFromEmbeddedResource("ArmorProficienciesList");
			//Todo:add list of weapons and armor from sqlitedb

		}

		public async static void LoadArmorList()
		{
			Debug.WriteLine("Starting to load armor");
			
			
			
		}

		public async static void LoadWeaponList()
		{ 
			
			
		}


	}
}

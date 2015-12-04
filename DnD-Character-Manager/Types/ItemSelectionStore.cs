using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Notifications;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DnD_Character_Manager.Types
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
			weaponJson = await JsonLoader.LoadJsonFromEmbeddedResource("WeaponProficienciesList");
			armorJson = await JsonLoader.LoadJsonFromEmbeddedResource("ArmorProficienciesList");
			weaponProfList.AddRange(JsonConvert.DeserializeObject<string[]>(weaponJson));
			armorProfList.AddRange(JsonConvert.DeserializeObject<string[]>(armorJson));

		}

		public async static void LoadArmorList()
		{
			Debug.WriteLine("Starting to load armor");
			
			string json = await JsonLoader.LoadJsonFromFile("ArmorList", ApplicationData.Current.RoamingFolder);
			if (!string.IsNullOrEmpty(json))
			{
				armor = JsonConvert.DeserializeObject<List<Armor>>(json);
			}
		}

		public async static void LoadWeaponList()
		{ 
			string json = await JsonLoader.LoadJsonFromFile("WeaponList", ApplicationData.Current.RoamingFolder);
			if (!string.IsNullOrEmpty(json))
			{
				weapons = JsonConvert.DeserializeObject<List<Weapon>>(json);
			}
		}


	}
}

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
			LoadArmorList();
			LoadWeaponList();
			
		}

		public async static void LoadArmorList()
		{
			Debug.WriteLine("Starting to load armor");
			string profJson = await JsonLoader.LoadJsonFromEmbeddedResource("WeaponProficienciesList.json");
			Debug.WriteLine("loaded armor proficieny json: \n" + profJson);
			string json = await JsonLoader.LoadJsonFromFile("ArmorList.json", ApplicationData.Current.RoamingFolder);
			if (!string.IsNullOrEmpty(json))
			{
				armor = JsonConvert.DeserializeObject<List<Armor>>(json);
			}
		}

		public async static void LoadWeaponList()
		{
			string profJson = await JsonLoader.LoadJsonFromEmbeddedResource("ArmorProficienciesList.json");
			Debug.WriteLine("loaded weapon proficieny json: \n" +profJson);
			string json = await JsonLoader.LoadJsonFromFile("WeaponList.json", ApplicationData.Current.RoamingFolder);
			if (!string.IsNullOrEmpty(json))
			{
				weapons = JsonConvert.DeserializeObject<List<Weapon>>(json);
			}
		}


	}
}

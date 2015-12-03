using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;

namespace DnD_Character_Manager.Types
{
	public static class ItemSelectionStore
	{
		public static List<Armor> armor { get;private set; }
		public static List<Weapon> weapons { get; private set; }

		static ItemSelectionStore()
		{
			armor = new List<Armor>();
			weapons = new List<Weapon>();
			LoadArmorList();
			LoadWeaponList();
		}

		public async static void LoadArmorList()
		{
			string json = await JsonLoader.LoadJsonFromFile("ArmorList.json", ApplicationData.Current.RoamingFolder);
			if (!string.IsNullOrEmpty(json))
			{
				armor = JsonConvert.DeserializeObject<List<Armor>>(json);
			}
		}

		public async static void LoadWeaponList()
		{
			string json = await JsonLoader.LoadJsonFromFile("WeaponList.json", ApplicationData.Current.RoamingFolder);
			if (!string.IsNullOrEmpty(json))
			{
				weapons = JsonConvert.DeserializeObject<List<Weapon>>(json);
			}
		}


	}
}

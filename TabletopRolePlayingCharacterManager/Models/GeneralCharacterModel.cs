using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	class GeneralCharacterModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string PhysicalDescription { get; set; }
		public int MaxHP { get; set; }
		public int CurrHP { get; set; }


		//Serialized Properties
		public string SkillsJson
		{
			get { return JsonConvert.SerializeObject(Skills); }
			set { Skills = JsonConvert.DeserializeObject<List<GenericSkill>>(value); }
		}

		public string AttributesJson
		{
			get { return JsonConvert.SerializeObject(Attributes); }
			set
			{
				Attributes = JsonConvert.DeserializeObject<Dictionary<string, int>>(value);
			}
		}
		//Properties that have to be serialized
		[Ignore]
		public List<GenericSkill> Skills { get; set; }
		[Ignore]
		public Dictionary<string, int> Attributes { get; set; }
	}
}

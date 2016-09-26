using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace TabletopRolePlayingCharacterManager.Models
{
	//The only things I need to store are the list of skills, Attributes, and Template title
	public class CharacterTemplate
	{
		public string TemplateName { get; set; } = "";
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

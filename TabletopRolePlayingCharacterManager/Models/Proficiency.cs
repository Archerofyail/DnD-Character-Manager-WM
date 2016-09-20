
using SQLite.Net;
using SQLite.Net.Attributes;
namespace TabletopRolePlayingCharacterManager.Models
{
	//This is for non-skill proficiencies, i.e. languages, weapon and armor, tools, etc.
	public class Proficiency
	{
		[PrimaryKey(), AutoIncrement]
		private int id { get; set; }
		private string name;
		private string description;
		private string category = "Miscellaneous";

		public string Name
		{
			get { return name; }
		}

		public string Description
		{
			get { return description; }
		}

		public string Category
		{
			get { return category; }
		}
	}
}
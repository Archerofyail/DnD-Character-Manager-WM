namespace TabletopRolePlayingCharacterManager.Models
{
	public class Proficiency
	{

		public Proficiency(ProficiencyType type, string name = "")
		{
			Type = type;
			Name = name;
		}


		public Proficiency()
		{
			
		}
		public string Name { get; set; }

		public ProficiencyType Type { get; set; }
	}
}

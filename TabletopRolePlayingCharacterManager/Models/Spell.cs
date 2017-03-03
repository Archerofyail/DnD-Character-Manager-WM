namespace TabletopRolePlayingCharacterManager.Models
{
	public class Spell
	{
		public string Name { get; set; }
		public int Level { get; set; }
		public bool HasVerbalComponent { get; set; }
		public bool HasSomaticComponent { get; set; }
		public string MaterialComponent { get; set; }
		public string Description { get; set; }
		public bool CanBeRitual { get; set; }
	}
}

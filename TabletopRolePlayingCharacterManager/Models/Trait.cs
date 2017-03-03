
namespace TabletopRolePlayingCharacterManager.Models
{
	public class Trait
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public StatIncrease StatBonus { get; set; }
		public bool IsActive { get; set; }
		public TraitSource Source { get; set; }

		public Trait(string name, string desc, TraitSource source):this(desc)
		{
			Name = name;
			Source = source;
		}

		public Trait(string desc)
		{
			Description = desc;
		}

		public Trait()
		{
			
		}
	}
}

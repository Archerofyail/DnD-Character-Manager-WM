
namespace TabletopRolePlayingCharacterManager.Models
{
	public class Trait
	{
		public string Description { get; set; }
		public StatIncrease StatBonus { get; set; }
		public bool IsActive { get; set; }


		public Trait(string desc)
		{
			Description = desc;
		}

		public Trait()
		{
			
		}
	}
}

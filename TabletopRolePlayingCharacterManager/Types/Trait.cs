
namespace TabletopRolePlayingCharacterManager.Types
{
	public class Trait
	{
		public string Description { get; set; }
		public IStatIncrease StatBonus { get; set; }
		public bool IsActive { get; set; }


		public Trait(string desc)
		{
			Description = desc;
		}
	}
}

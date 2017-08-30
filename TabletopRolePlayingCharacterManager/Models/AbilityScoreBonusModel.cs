namespace TabletopRolePlayingCharacterManager.Models
{
	public class AbilityScoreBonusModel
	{
		public MainStatType Stat { get; set; } = MainStatType.Strength;
		public int Bonus { get; set; } = 1;
	}
}

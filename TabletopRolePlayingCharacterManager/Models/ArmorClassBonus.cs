namespace TabletopRolePlayingCharacterManager.Models
{
	public class ArmorClassBonus : StatIncrease
	{
		private int bonus = 1;
		private static string _bonusName = "AC";
		private Character5E character;

		public override void AddBonus()
		{
			character.ArmorClass += bonus;
		}

		public override void RemoveBonus()
		{
			character.ArmorClass -= bonus;
		}
	}
}

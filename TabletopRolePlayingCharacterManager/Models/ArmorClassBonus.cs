namespace TabletopRolePlayingCharacterManager.Models
{
	public class ArmorClassBonus : StatIncrease
	{
		public ArmorClassBonus(int bonus, Character5E character)
		{
			Bonus = bonus;
			Character = character;
		}
		public override void AddBonus()
		{
			Character.ArmorClass += Bonus;
		}

		public override void RemoveBonus()
		{
			Character.ArmorClass -= Bonus;
		}
	}
}

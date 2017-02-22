using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.Types
{
	class ArmorClassBonus : StatIncrease
	{
		private int bonus = 1;
		private static string bonusName = "AC";
		private Character5E character;

		public Character5E Character
		{
			get { return character; }

			set { character = value; }
		}

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

using System;
using TabletopRolePlayingCharacterManager;
namespace TabletopRolePlayingCharacterManager.Types
{
	public class Weapon
	{

		public string Name { get; set; }
		public WeaponType WeaponType { get; set; }
		public Damage Damage { get; set; } = new Damage();
		public string Description { get; set; }
		public Tuple<MainStatType, int> MainStat { get; set; } = new Tuple<MainStatType, int>(MainStatType.Strength, 10);
		public int AttackBonus { get; set; }
		public int RollAttack()
		{
			var attack = 0;
			attack += Utility.Rand.Next(1, 21);
			attack += AttackBonus;
			return 10;
		}
	}
}

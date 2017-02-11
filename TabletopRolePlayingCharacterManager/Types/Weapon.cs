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
		public MainStatType MainStat { get; set; } = MainStatType.Strength;
		public int AttackBonus { get; set; }
		public int RollAttack(int AbilityScoreBonus)
		{
			var attack = 0;
			attack += Utility.Rand.Next(1, 21);
			attack += AttackBonus;
			return 10;
		}
	}
}

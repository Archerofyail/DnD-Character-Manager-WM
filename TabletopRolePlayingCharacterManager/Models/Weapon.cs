namespace TabletopRolePlayingCharacterManager.Models
{
	public class Weapon : Item
	{

		public WeaponRangeType WeaponRangeType { get; set; }
		public Damage Damage { get; set; }
		public Damage Damage2 { get; set; }
		public MainStatType MainStat { get; set; } = MainStatType.Strength;
		public int AttackBonus { get; set; }
		public bool IsProficient { get; set; }
		public Weapon()
		{
			ItemType = ItemType.Weapon;
		}

		public int RollAttack(int abilityScoreBonus)
		{
			var attack = 0;
			attack += Utility.Rand.Next(1, 21);
			attack += AttackBonus;
			return attack;
		}
	}
}

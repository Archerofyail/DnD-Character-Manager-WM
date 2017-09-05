namespace TabletopRolePlayingCharacterManager.Models
{
	public class Spell
	{
		public string Name { get; set; }
		public int Level { get; set; }
		public SpellSchool School { get; set; }
		public string Range { get; set; }
		public string RangeType { get; set; }
		public bool HasVerbalComponent { get; set; }
		public bool HasSomaticComponent { get; set; }
		public string MaterialComponent { get; set; }
		public string Description { get; set; }
		public string AtHigherLevelsDescription { get; set; }
		public bool CanBeRitual { get; set; }
		public string Target { get; set; }
		public bool IsAttack { get; set; }
		//True if the character needs to roll, otherwise it's a saving throw for the target
		public bool IsAttackRoll { get; set; }
		#region SavingThrowProperties

		public MainStatType SavingThrowAttribute;
		public string SavingThrowEffect;
		#endregion
		public Damage Damage { get; set; } = new Damage();
		public Damage Damage2 { get; set; } = new Damage();
		public bool AddAbilityModToDamage { get; set; }
		public string DamageType { get; set; }
		public Damage HigherLevelDamage { get; set; } = new Damage();
	}
}

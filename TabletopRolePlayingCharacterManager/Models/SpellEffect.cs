namespace TabletopRolePlayingCharacterManager.Types
{
	public class SpellEffect
	{
		public string Name { get; set; }
		public SpellEffectType EffectType { get; set; } = SpellEffectType.Enemy;
		public string Description { get; set; }
	}
}

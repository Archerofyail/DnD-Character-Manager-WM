namespace TabletopRolePlayingCharacterManager.Types
{
	public class Skill
	{

		public Skill(string name, int abilityBonus, int profBonus, MainStatType mStat, bool isProficient)
		{
			Name = name;
			MainStat = mStat;
			IsProficient = isProficient;
			CalculateBonus(abilityBonus, profBonus);
		}
		public string Name { get; set; }
		public MainStatType MainStat { get; set; }
		public int Bonus { get; set; }
		public bool IsProficient { get; set; }
		public void CalculateBonus(int mainStatBonus, int proficiencyBonus)
		{
			Bonus = IsProficient ? mainStatBonus + proficiencyBonus : mainStatBonus;
		}
	}
}

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Skill
	{

		public Skill(string name, int abilityBonus, int profBonus, MainStatType mStat, bool isProficient)
		{
			Name = name;
			MainStat = mStat;
			IsProficient = isProficient;
			CalculateBonus(abilityBonus, profBonus);
			this.abilityBonus = abilityBonus;
			this.profBonus = profBonus;
		}

		public Skill()
		{
			
		}


		private int abilityBonus;
		private int profBonus;
		public string Name { get; set; }
		public MainStatType MainStat { get; set; }
		public int Bonus { get; set; }
		public bool IsProficient { get; set; }
		public void CalculateBonus(int mainStatBonus, int proficiencyBonus)
		{
			abilityBonus = mainStatBonus;
			profBonus = proficiencyBonus;
			Bonus = IsProficient ? mainStatBonus + proficiencyBonus : mainStatBonus;
		}
		//Uses previously given ability and proficiency bonuses
		public void CalculateBonus()
		{
			Bonus = IsProficient ? abilityBonus + profBonus : abilityBonus;
		}
	}
}

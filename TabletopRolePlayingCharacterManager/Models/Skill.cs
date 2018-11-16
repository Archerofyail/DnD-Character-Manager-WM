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
			this._abilityBonus = abilityBonus;
			this._profBonus = profBonus;
		}

		public Skill()
		{
			
		}


		private int _abilityBonus;
		private int _profBonus;
		public string Name { get; set; }
		public MainStatType MainStat { get; set; }
		public int Bonus { get; set; }
		public bool IsProficient { get; set; }
		public void CalculateBonus(int mainStatBonus, int proficiencyBonus)
		{
			_abilityBonus = mainStatBonus;
			_profBonus = proficiencyBonus;
			Bonus = IsProficient ? mainStatBonus + proficiencyBonus : mainStatBonus;
		}
		//Uses previously given ability and proficiency bonuses
		public void CalculateBonus()
		{
			Bonus = IsProficient ? _abilityBonus + _profBonus : _abilityBonus;
		}
	}
}

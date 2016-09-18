namespace TabletopRolePlayingCharacterManager
{
	public struct Skill
	{
		public string Name { get; private set; }
		public int Bonus { get; private set; }
		public readonly MainStat MainStatType;
		public bool IsProficient { get; private set; }

		public Skill(string name, MainStat mainStatType, int mainStat, int proficiencyBonus, bool isProficient = false)
		{
			Name = name;
			MainStatType = mainStatType;
			Bonus = 0;
			IsProficient = isProficient;
			CalculateBonus(mainStat, proficiencyBonus);
		}

		public void CalculateBonus(int mainStat, int proficiencyBonus)
		{
			Bonus = 0;
			Bonus += Utility.CalculateMainStatBonus(mainStat);
			if (IsProficient)
			{
				Bonus += proficiencyBonus;
			}
		}

		public void ChangeProficiency(bool isProf)
		{
			IsProficient = isProf;
		}
	}

}

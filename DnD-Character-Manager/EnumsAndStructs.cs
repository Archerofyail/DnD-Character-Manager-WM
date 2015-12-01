using System.Collections.Generic;

namespace DnD_Character_Manager
{

	public enum MainStat
	{
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma
	}

	struct Skill
	{
		public string Name { get; private set; }
		public int Bonus { get; private set; }
		public readonly MainStat MainStatType;
		public bool IsProficient { get; private set; }

		public Skill(string name, MainStat mainStatType, int mainStat, int proficiencyBonus, bool isProficient)
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

	public static class Utility
	{

		public static readonly Dictionary<MainStat, string> shorthandStatString = new Dictionary<MainStat, string>
		{
			{MainStat.Strength, "STR" },
			{MainStat.Dexterity, "DEX" },
			{MainStat.Constitution, "CON" },
			{MainStat.Intelligence, "INT" },
			{MainStat.Wisdom, "WIS" },
			{MainStat.Charisma, "CHA" }
		};
		public static int CalculateMainStatBonus(int mainStat)
		{
			int bonus = 0;
			mainStat -= 10;
			bonus /= 2;
			return bonus;
		}
	}
}


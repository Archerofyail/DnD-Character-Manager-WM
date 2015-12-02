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

	public enum ArmorType
	{
		Light = 55,
		Medium = 2,
		Heavy = 0
	}

	public enum WeaponType
	{
		Simple,
		Martial
	}
	//TODO: Give the user the option of saving the custom item to disk during character creation.

	//TODO: Each section (Regular Items, Weapons, Armor, Races, subraces, etc.) will get their own class that manages the list of possible choices the player has. These classes will also handle adding new choices to each list and saving these. the Model of the character creation page will then take these and present them to the ViewModel, which will of course, give the View access to them. To add them. The view will have a + button or something. Then a menu flyout will appear, and the user will be able to choose what the race/item/weapon/armor/whatever, is called, be able to choose what it affects, if anything, and a description, to describe things that don't happen to the character, if any. It will then tell the viewmodel that a new item was created, then the viewmodel will tell the model that. Then, finally, the model will tell the class that manages that type of item to add it to the list, and save that list to disk.
	public struct Skill
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

	public struct Weapon
	{
		public WeaponType WeaponType { get; private set; }
		public int AttackBonus { get; private set; }
		public int DamageBonus { get; private set; }
		public int MagicLevel { get; private set; }
		public List<string> MagicEffects { get; private set; }

		public Weapon(WeaponType weaponType, int strengthBonus, int proficiencyBonus, bool isProficient, int magicLevel, List<string> magicEffects)
		{
			WeaponType = weaponType;
			AttackBonus = strengthBonus + proficiencyBonus + magicLevel;
			DamageBonus = strengthBonus + magicLevel;
			MagicLevel = magicLevel;
			MagicEffects = new List<string>(magicEffects);
		}

		public void RecalculateBonuses(int strengthBonus, int proficiencyBonus, bool isProficient, int magicLevel)
		{
			AttackBonus = strengthBonus + proficiencyBonus + magicLevel;
			DamageBonus = strengthBonus + magicLevel;
		}

		public void SetMagicLevel(int magicLevel)
		{
			MagicLevel = magicLevel;
		}
	}

	public struct Armor
	{
		public ArmorType ArmorType;
		private int baseAC;
		public int ArmorClass { get { return MagicBonus + baseAC; } }
		public int MagicBonus { get; private set; }

		public Armor(ArmorType armorType, int armorClass, int magicLevel)
		{
			ArmorType = armorType;
			baseAC = armorClass;
			MagicBonus = magicLevel;
		}

		public void UpdateStats(ArmorType armorType, int baseAC, int magicLevel)
		{
			armorType = ArmorType;
			this.baseAC = baseAC;
			MagicBonus = magicLevel;
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

		public static int CalculateProficiencyBonus(int level)
		{
			int bonus = 0;
			return bonus;
		}
	}
}


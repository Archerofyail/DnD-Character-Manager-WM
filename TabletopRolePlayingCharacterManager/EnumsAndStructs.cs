using System;
using System.Collections.Generic;
using Windows.Devices.Bluetooth.Advertisement;

namespace TabletopRolePlayingCharacterManager
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

	//TODO: Give the user the option of saving the custom items during character creation.

	//TODO: Each section (Regular Items, Weapons, Armor, Races, subraces, etc.) will get their own class that manages the list of possible choices the player has. These classes will also handle adding new choices to each list and saving these. the Model of the character creation page will then take these and present them to the ViewModel, which will of course, give the View access to them. To add them. The view will have a + button or something. Then a menu flyout will appear, and the user will be able to choose what the race/item/weapon/armor/whatever, is called, be able to choose what it affects, if anything, and a description, to describe things that don't happen to the character, if any. It will then tell the viewmodel that a new item was created, then the viewmodel will tell the model that. Then, finally, the model will tell the class that manages that type of item to add it to the list, and save that list to disk.

	//TODO: Give up hope of dealing with attack/damage rolls until you have everything else working properly
	//Races need a title(AKA a name), and a description. They also need a list of things to add to the character traits. This also applies to subraces.
	//Classes and subclasses need the same thing, but also need stuff specific to class features

	//For displaying skills during character creation, you need the name, and the ability score it's associated with.
	//For skills within the character class, you want the name{string}, the ability score{MainStat}, and whether they're proficient with it{bool}

	

	public class Weapon : IItem
	{
		public string WeaponType { get; private set; } //Simple or martial in 5E, though this could also be a specific type such as a club, or a rapier or something (or both?)
		public int AttackBonus { get; private set; }
		public int DamageBonus { get; private set; }
		public int MagicLevel { get; private set; }
		public List<string> MagicEffects { get; private set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Weapon(string weaponType, int strengthBonus, int proficiencyBonus, bool isProficient, int magicLevel,
			List<string> magicEffects)
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

	public class Armor
	{
		public ArmorType ArmorType;
		private int baseAC;
		public int ArmorClass { get { return MagicBonus + baseAC; } }
		public int MagicBonus { get; private set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Equipped { get; set; }

		public Armor(string name, ArmorType armorType, int armorClass, int magicLevel)
		{
			Name = name;
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
			bonus = mainStat / 2;
			return bonus;
		}

		public static int CalculateProficiencyBonus(int level)
		{
			int bonus = 0;
			if (level <= 8)
			{
				bonus = 3;
			}
			else if (level <= 12)
			{
				bonus = 4;
			}
			return bonus;
		}
	}
}


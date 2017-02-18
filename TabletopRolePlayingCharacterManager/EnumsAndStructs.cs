using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager
{

	public enum MainStatType
	{
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma
	}

	public enum DieType
	{
		D3,
		D4,
		D6,
		D8,
		D10,
		D12,
		D20,
		D100
	}

	public enum WeaponType
	{
		Melee,
		Ranged
	}

	public enum MessageType
	{
		Object
	}

	public enum CurrencyType
	{
		Copper,
		Silver,
		Electrum,
		Gold,
		Platinum
	}

	public enum SpellEffectType
	{
		Self,
		Enemy,
		Area
		
	}

	public enum ItemType
	{
		Pack,
		Item,
		Armor,
		Weapon
	}

	//TODO: Give the user the option of saving the custom items during character creation.

	//TODO: Each section (Regular Items, Weapons, Armor, Races, subraces, etc.) will get their own class that manages the list of possible choices the player has. These classes will also handle adding new choices to each list and saving these. the Model of the character creation page will then take these and present them to the ViewModel, which will of course, give the View access to them. To add them. The view will have a + button or something. Then a menu flyout will appear, and the user will be able to choose what the race/item/weapon/armor/whatever, is called, be able to choose what it affects, if anything, and a description, to describe things that don't happen to the character, if any. It will then tell the viewmodel that a new item was created, then the viewmodel will tell the model that. Then, finally, the model will tell the class that manages that type of item to add it to the list, and save that list to disk.

	//TODO: Give up hope of dealing with attack/damage rolls until you have everything else working properly
	//Races need a title(AKA a name), and a description. They also need a list of things to add to the character traits. This also applies to subraces.
	//Classes and subclasses need the same thing, but also need stuff specific to class features

	//For displaying skills during character creation, you need the name, and the ability score it's associated with.
	//For skills within the character class, you want the name{string}, the ability score{MainStatType}, and whether they're proficient with it{bool}


	public class Message<T>
	{
		public MessageType MessageType { get; private set; }
		public T Data { get; private set; }
		public Type DestVM { get; private set; }

		public Message(MessageType message, T data, Type destVM)
		{
			MessageType = message;
			Data = data;
			DestVM = destVM;
		}
	}

	public static class Utility
	{

		public static readonly Dictionary<MainStatType, string> ShorthandStatStrings = new Dictionary<MainStatType, string>
		{
			{MainStatType.Strength, "STR" },
			{MainStatType.Dexterity, "DEX" },
			{MainStatType.Constitution, "CON" },
			{MainStatType.Intelligence, "INT" },
			{MainStatType.Wisdom, "WIS" },
			{MainStatType.Charisma, "CHA" }
		};

		public static Random Rand = new Random();

		public static int CalculateMainStatBonus(int mainStat)
		{
			int bonus = 0;
			mainStat -= 10;
			bonus = mainStat / 2;
			return bonus;
		}

		public static int CalculateProficiencyBonus(int level)
		{
			int bonus = 2;
			if (level <=4)
			{
				bonus = 2;
			}
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


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TabletopRolePlayingCharacterManager.Models
{
	//TODO: Add a dice picker
	public class Damage
	{
		//Die size probable shouldn't be more than a D12
		public Dictionary<DieType, int> Dice { get; set; } = new Dictionary<DieType, int>();
		public int Bonus { get; set; }

		public Damage(int bonus)
		{
			
			Bonus = bonus;
		}

		public Damage()
		{

		}
		public int RollDamage()
		{
			var finalRoll = 0;
			foreach (var die in Dice)
			{
				var dieSize = int.Parse(die.Key.ToString().Substring(1));
				for (var i = 0; i < die.Value; i++)
				{
					finalRoll += Utility.Rand.Next(1, dieSize + 1);
				}
			}

			finalRoll += Bonus;

			return finalRoll;
		}

		public override string ToString()
		{
			var text = "";
			if (Dice.Count > 0)
			{
				foreach (var die in Dice)
				{
					text += die.Value + "d" + die.Key.ToString().Substring(1);
				}
			}
			if (Bonus > 0)
			{

				text += " + " + Bonus;
			}
			return text;
		}
		/// <summary>
		/// Parses a string and extracts the dice and bonus damage, then replaces the current values in this object
		/// </summary>
		/// <param name="text"></param>
		public void ParseText(string text)
		{
			var matches = Regex.Match(text, @"(\d)([dD]\d{1,3})\s?[\+-]\s?(\d{1,3})");
			Debug.WriteLine("Matches: " + matches.Value);
			for (var i = 0; i < matches.Groups.Count; i++)
			{
				Debug.WriteLine("match " + i + " is " + matches.Groups[i].Value);
			}
			if (matches.Success && matches.Groups.Count >= 3)
			{
				var dieType = DieType.D4;
				if (int.TryParse(matches.Groups[1].Value, out int numDice))
				{
					if (Enum.TryParse(matches.Groups[2].Value.ToUpper(), out dieType))
					{
						Dice.Clear();
						Dice.Add(dieType, numDice);
						
					}
				}
				if (int.TryParse(matches.Groups[3].Value, out int numBonus))
				{
					Bonus = numBonus;
				}
			}
		}
	}
}

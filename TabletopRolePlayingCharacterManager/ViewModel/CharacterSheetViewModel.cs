﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	//If the CharacterManager's current character is empty when loading this page, assume a new character was created.
	public class CharacterSheetViewModel : ViewModelBase
	{
		Character5E character = new Character5E();

		public CharacterSheetViewModel()
		{
			character = CharacterManager.CurrentCharacter;
		}
		#region General

		public string Name
		{
			get { return character.Name; }
			set
			{
				character.Name = value;
				RaisePropertyChanged();
			}
		}
		public string Race
		{
			get
			{
				return character.Race;
			}
			set
			{
				character.Race = value;
				RaisePropertyChanged();
			}
		}
		public string Class
		{
			get
			{
				return character.Class;
			}
			set
			{
				character.Class = value;
				RaisePropertyChanged();
			}
		}
		public string Alignment
		{
			get
			{
				return character.Alignment;
			}
			set
			{
				character.Alignment = value;
				RaisePropertyChanged();
			}
		}
		public string Level
		{
			get
			{
				return character.Level.ToString();
			}
			set
			{
				var result = character.Level;
				if (int.TryParse(value, out result))
				{
					character.Level = result;
				}

				RaisePropertyChanged();
			}
		}
		public string Experience
		{
			get
			{
				return character.Experience.ToString();
			}
			set
			{
				var result = character.Experience;
				if (int.TryParse(value, out result))
				{
					character.Experience = result;
				}

				RaisePropertyChanged();
			}
		}
		#endregion

		#region PhysicalTraits

		public string Age
		{
			get
			{
				return character.Age.ToString();
			}
			set
			{
				var result = character.Age;
				if (int.TryParse(value, out result))
				{
					character.Age = result;
				}

				RaisePropertyChanged();
			}
		}

		public string Height
		{
			get
			{
				return character.Height;
			}
			set
			{
				character.Height = value;
				RaisePropertyChanged();
			}
		}


		public string Weight
		{
			get
			{
				return character.Weight;
			}
			set
			{
				character.Weight = value;
				RaisePropertyChanged();
			}
		}

		public string Eyes
		{
			get
			{
				return character.Eyes;
			}
			set
			{
				character.Eyes = value;
				RaisePropertyChanged();
			}
		}

		public string Skin
		{
			get
			{
				return character.Skin;
			}
			set
			{
				character.Skin = value;
				RaisePropertyChanged();
			}
		}

		public string Hair
		{
			get
			{
				return character.Hair;
			}
			set
			{
				character.Hair = value;
				RaisePropertyChanged();
			}
		}


		#endregion

		#region AbilityScores
		public string Strength
		{
			get { return character.AbilityScores[MainStat.Strength].ToString(); }
			set
			{
				var result = character.AbilityScores[MainStat.Strength];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStat.Strength] = result;
				}

				RaisePropertyChanged();
			}
		}
		public string Dexterity
		{
			get { return character.AbilityScores[MainStat.Dexterity].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStat.Dexterity];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStat.Dexterity] = result;
				}

				RaisePropertyChanged();
			}
		}
		public string Constitution
		{
			get { return character.AbilityScores[MainStat.Constitution].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStat.Constitution];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStat.Constitution] = result;
				}

				RaisePropertyChanged();
			}
		}
		public string Intelligence
		{
			get { return character.AbilityScores[MainStat.Intelligence].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStat.Intelligence];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStat.Intelligence] = result;
				}

				RaisePropertyChanged();
			}
		}
		public string Wisdom
		{
			get { return character.AbilityScores[MainStat.Wisdom].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStat.Wisdom];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStat.Wisdom] = result;
				}

				RaisePropertyChanged();
			}
		}
		public string Charisma
		{
			get { return character.AbilityScores[MainStat.Charisma].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStat.Charisma];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStat.Charisma] = result;
				}

				RaisePropertyChanged();
			}
		}

		#region AbilityModifiers

		#endregion

		#region AbilityProficiencies
		public bool StrProficiency
		{
			get { return true; }
		}
		#endregion
		#endregion
	}
}

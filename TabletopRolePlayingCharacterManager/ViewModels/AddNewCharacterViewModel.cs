using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;
using System.Diagnostics;
using GalaSoft.MvvmLight;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	internal class AddNewCharacterPageViewModel : ViewModelBase
	{
		private readonly Random randd6 = new Random();
		private Character5E character = new Character5E();

		#region CharacterData
		

		public string CharName
		{
			get { return character.Name; }
			set
			{
				character.Name = value;
				RaisePropertyChanged();

			}
		}


		public string Age
		{
			get
			{
				return character.Age.ToString();
			}
			set
			{
				if (int.TryParse(value, out int result))
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


		public string Eye
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

		public int StrengthStat
		{
			get { return character.AbilityScores[MainStatType.Strength]; }
			set
			{
				character.AbilityScores[MainStatType.Strength] = value;
				RaisePropertyChanged();
			}
		}

		public int DexterityStat
		{
			get { return character.AbilityScores[MainStatType.Dexterity]; }
			set
			{
				character.AbilityScores[MainStatType.Dexterity] = value;
				RaisePropertyChanged();
			}
		}

		public int ConstitutionStat
		{
			get { return character.AbilityScores[MainStatType.Constitution]; }
			set
			{
				character.AbilityScores[MainStatType.Constitution] = value;
				RaisePropertyChanged();
			}
		}

		public int IntelligenceStat
		{
			get { return character.AbilityScores[MainStatType.Intelligence]; }
			set
			{
				character.AbilityScores[MainStatType.Intelligence] = value;
				RaisePropertyChanged();
			}
		}

		public int WisdomStat
		{
			get { return character.AbilityScores[MainStatType.Wisdom]; }
			set
			{
				character.AbilityScores[MainStatType.Wisdom] = value;
				RaisePropertyChanged();
			}
		}

		public int CharismaStat
		{
			get { return character.AbilityScores[MainStatType.Charisma]; }
			set
			{
				character.AbilityScores[MainStatType.Charisma] = value;
				RaisePropertyChanged();
			}
		}
		private int selectedSubClassId;
		public int SelectedSubClass
		{
			get { return selectedSubClassId; }
			set
			{
				selectedSubClassId = value;
				RaisePropertyChanged();
			}
		}
	
		#endregion
		#region Lists
		private readonly ObservableCollection<Skill> skills = new ObservableCollection<Skill>();

		public ObservableCollection<Skill> Skills
		{
			get
			{
				if (skills.Count == 0)
				{
					foreach (var skill in character.Skills)
					{
						Debug.WriteLine("Added skill to list: " + skill.Name);
						skills.Add(skill);
					}
				}
				return skills;
			}
		}

		private ObservableCollection<RaceViewModel> races = new ObservableCollection<RaceViewModel>();

		public ObservableCollection<RaceViewModel> Races
		{
			get
			{
				if (races.Count == 0)
				{
					foreach (var race in CharacterManager.RacialBonuses)
					{
						if (race.ParentRace == "")
						{
							races.Add(new RaceViewModel(race));
						}
					}
				}
				return races;
			}
		}

		private int selectedRaceId;

		public int SelectedRaceIndex
		{
			get { return selectedRaceId; }
			set
			{
				selectedRaceId = value;
				RaisePropertyChanged();
				RaisePropertyChanged("SelectedRace");
			}
		}

		private RaceViewModel SelectedRace
		{
			get { return Races[SelectedRaceIndex]; }
		}


		public ObservableCollection<RaceViewModel> Subraces
		{
			get
			{
				var subraces = new ObservableCollection<RaceViewModel>();
					foreach (var race in CharacterManager.RacialBonuses)
					{
						if (race.ParentRace == SelectedRace.racialBonuses.Race)
						{
							subraces.Add(new RaceViewModel(race));
						}
					}
				return subraces;
			}
		}

		private int selectedSubRaceId;

		public int SelectedSubRace
		{
			get { return selectedSubRaceId; }
			set
			{
				selectedSubRaceId = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<ClassViewModel> classes = new ObservableCollection<ClassViewModel>();

		public ObservableCollection<ClassViewModel> Classes
		{
			get
			{
				if (classes.Count == 0)
				{
					foreach (var cl in CharacterManager.ClassBonuses)
					{
						classes.Add(new ClassViewModel(cl));
					}
				}
				return classes;
			}
		}

		private int selectedClassId;

		public int SelectedClass
		{
			get { return selectedClassId; }
			set
			{
				selectedClassId = value;
				RaisePropertyChanged();
			}
		}



		private static ObservableCollection<string> _alignments = new ObservableCollection<string>() { "Chaotic Evil", "Neutral Evil", "Lawful Evil", "Chaotic Neutral", "Neutral", "Lawful Neutral", "Chaotic Good", "Neutral Good", "Lawful Good" };

		public ObservableCollection<string> Alignments => _alignments;

		public int SelectedAlignment { get; set; } = -1;

		



		#endregion


		private string subclassChoiceStatement = "Choose your subclass{default}";

		public string SubclassChoiceStatement => subclassChoiceStatement;


		private ObservableCollection<string> rolledAbilityScores = new ObservableCollection<string>();

		public ObservableCollection<string> RolledAbilityScores => rolledAbilityScores;

		public void RollAbilityScores()
		{

			rolledAbilityScores.Clear();
			for (var i = 0; i < 6; i++)
			{
				int final;
				var rolls = new List<int>
				{
					randd6.Next(1, 7),
					randd6.Next(1, 7),
					randd6.Next(1, 7),
					randd6.Next(1, 7)
				};
				rolls.Sort();
				final = rolls[1] + rolls[2] + rolls[3];
				rolledAbilityScores.Add(final + "");


			}
		}

		#region UIControls



		public Visibility RolledScoresExist
		{
			get
			{
				if (rolledAbilityScores.Count > 0)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
		}
		#endregion

		public async void CreateCharacter()
		{
		
		}

		public AddNewCharacterPageViewModel()
		{
		}
	}
}

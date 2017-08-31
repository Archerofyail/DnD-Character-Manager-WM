using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	internal class AddNewCharacterPageViewModel : ViewModelBase
	{
		private readonly Random randd6 = new Random();
		private Character5E character = CharacterManager.GetNewChar();

		public AddNewCharacterPageViewModel()
		{
			
		}

		#region CharacterData


		public string CharName
		{
			get => character.Name;
			set
			{
				character.Name = value;
				RaisePropertyChanged();

			}
		}

		public string Age
		{
			get => character.Age.ToString();
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
			get => character.Height;
			set
			{
				character.Height = value;
				RaisePropertyChanged();
			}
		}

		public string Weight
		{
			get => character.Weight;
			set
			{
				character.Weight = value;
				RaisePropertyChanged();
			}
		}

		public string Hair
		{
			get => character.Hair;
			set
			{
				character.Hair = value;
				RaisePropertyChanged();
			}
		}

		public string Skin
		{
			get => character.Skin;
			set
			{
				character.Skin = value;
				RaisePropertyChanged();
			}
		}

		public string Eye
		{
			get => character.Eyes;
			set
			{
				character.Eyes = value;
				RaisePropertyChanged();
			}
		}

		public int StrengthStat
		{
			get => character.AbilityScores[MainStatType.Strength];
			set
			{
				character.AbilityScores[MainStatType.Strength] = value;
				RaisePropertyChanged();
			}
		}

		public int DexterityStat
		{
			get => character.AbilityScores[MainStatType.Dexterity];
			set
			{
				character.AbilityScores[MainStatType.Dexterity] = value;
				RaisePropertyChanged();
			}
		}

		public int ConstitutionStat
		{
			get => character.AbilityScores[MainStatType.Constitution];
			set
			{
				character.AbilityScores[MainStatType.Constitution] = value;
				RaisePropertyChanged();
			}
		}

		public int IntelligenceStat
		{
			get => character.AbilityScores[MainStatType.Intelligence];
			set
			{
				character.AbilityScores[MainStatType.Intelligence] = value;
				RaisePropertyChanged();
			}
		}

		public int WisdomStat
		{
			get => character.AbilityScores[MainStatType.Wisdom];
			set
			{
				character.AbilityScores[MainStatType.Wisdom] = value;
				RaisePropertyChanged();
			}
		}

		public int CharismaStat
		{
			get => character.AbilityScores[MainStatType.Charisma];
			set
			{
				character.AbilityScores[MainStatType.Charisma] = value;
				RaisePropertyChanged();
			}
		}
		private int selectedSubClassId;
		public int SelectedSubClass
		{
			get => selectedSubClassId;
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


		#region Race
		private int selectedRaceIndex = -1;

		public int SelectedRaceIndex
		{
			get => selectedRaceIndex;
			set
			{
				selectedRaceIndex = value;
				RaisePropertyChanged();
				RaisePropertyChanged("SelectedRace");
				RaiseAllSelectedRaceChanged();
			}
		}

		#region SelectedRace

		private RaceViewModel SelectedRace => (SelectedRaceIndex >= 0 && SelectedRaceIndex < Races.Count) ? Races[SelectedRaceIndex] : new RaceViewModel();


		public ObservableCollection<ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>> SelectedRaceAbilityScoreBonuses => SelectedRace.AbilityScoreBonuses;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedRaceLanguageProfs => SelectedRace.LanguageProficiencies;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedRaceWeaponProfs => SelectedRace.WeaponProficiencies;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedRaceArmorProfs => SelectedRace.ArmorProficiencies;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedRaceToolProfs => SelectedRace.ToolProficiencies;

		public ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> SelectedRaceTraits => SelectedRace.Traits;

		void RaiseAllSelectedRaceChanged()
		{
			RaisePropertyChanged("SelectedRaceAbilityScoreBonuses");
			RaisePropertyChanged("SelectedRaceLanguageProfs");
			RaisePropertyChanged("SelectedRaceWeaponProfs");
			RaisePropertyChanged("SelectedRaceArmorProfs");
			RaisePropertyChanged("SelectedRaceToolProfs");
			RaisePropertyChanged("Subraces");
			RaisePropertyChanged("SelectedRaceTraits");
		}
		#endregion

		#region SelectedSubrace

		private int selectedSubRaceIndex = -1;

		public int SelectedSubRaceIndex
		{
			get => selectedSubRaceIndex;
			set
			{
				selectedSubRaceIndex = value;
				RaisePropertyChanged();
				RaiseAllSelectedSubraceChanged();
			}
		}

		#endregion

		private RaceViewModel SelectedSubrace => (selectedSubRaceIndex >= 0 && selectedSubRaceIndex < Subraces.Count) ? Subraces[selectedSubRaceIndex] : new RaceViewModel();


		public ObservableCollection<ChoiceViewModel<AbilityScoreBonusModel, AbilityScoreBonusViewModel>> SelectedSubraceAbilityScoreBonuses => SelectedSubrace.AbilityScoreBonuses;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedSubraceLanguageProfs => SelectedSubrace.LanguageProficiencies;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedSubraceWeaponProfs => SelectedSubrace.WeaponProficiencies;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedSubraceArmorProfs => SelectedSubrace.ArmorProficiencies;

		public ObservableCollection<ProficiencyChoiceViewModel> SelectedSubraceToolProfs => SelectedSubrace.ToolProficiencies;

		public ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> SelectedSubraceTraits => SelectedSubrace.Traits;

		void RaiseAllSelectedSubraceChanged()
		{
			RaisePropertyChanged("SelectedSubrace");
			RaisePropertyChanged("SelectedSubraceAbilityScoreBonuses");
			RaisePropertyChanged("SelectedSubraceLanguageProfs");
			RaisePropertyChanged("SelectedSubraceWeaponProfs");
			RaisePropertyChanged("SelectedSubraceArmorProfs");
			RaisePropertyChanged("SelectedSubraceToolProfs");
			RaisePropertyChanged("SelectedSubraceTraits");
		}
		#endregion


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

		private int selectedClassIndex;

		public int SelectedClassIndex
		{
			get => selectedClassIndex;
			set
			{
				selectedClassIndex = value;
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

		#region Commands

		public ICommand EndCharacterCreation => new RelayCommand(FinishedCharacterCreation);
		
		public void FinishedCharacterCreation()
		{
			foreach (var trait in SelectedRaceTraits)
			{

				character.Traits.AddRange(trait.GetSelectedItems());

			}
			foreach (var bonus in SelectedRaceAbilityScoreBonuses)
			{
				foreach (var bonus1 in bonus.GetSelectedItems())
				{
					character.AbilityScores[bonus1.Stat] += bonus1.Bonus;
				}
			}

			foreach (var prof in SelectedRaceLanguageProfs)
			{
				character.Languages.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceWeaponProfs)
			{
				character.WeaponProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceArmorProfs)
			{
				character.ArmorProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceToolProfs)
			{
				character.Proficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceToolProfs)
			{
				character.Proficiencies.AddRange(prof.GetSelectedItems());
			}

			foreach (var trait in SelectedSubraceTraits)
			{

				character.Traits.AddRange(trait.GetSelectedItems());

			}
			foreach (var bonus in SelectedSubraceAbilityScoreBonuses)
			{
				foreach (var bonus1 in bonus.GetSelectedItems())
				{
					character.AbilityScores[bonus1.Stat] += bonus1.Bonus;
				}
			}

			foreach (var prof in SelectedSubraceLanguageProfs)
			{
				character.Languages.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceWeaponProfs)
			{
				character.WeaponProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceArmorProfs)
			{
				character.ArmorProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceToolProfs)
			{
				character.Proficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceToolProfs)
			{
				character.Proficiencies.AddRange(prof.GetSelectedItems());
			}
		}

		#endregion




	}
}

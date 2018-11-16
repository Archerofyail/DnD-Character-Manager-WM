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
		private readonly Random _randd6 = new Random();
		private Character5E _character = CharacterManager.GetNewChar();

		public AddNewCharacterPageViewModel()
		{
			
		}

		#region CharacterData


		public string CharName
		{
			get => _character.Name;
			set
			{
				_character.Name = value;
				RaisePropertyChanged();

			}
		}

		public string Age
		{
			get => _character.Age.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.Age = result;
				}
				RaisePropertyChanged();
			}
		}

		public string Height
		{
			get => _character.Height;
			set
			{
				_character.Height = value;
				RaisePropertyChanged();
			}
		}

		public string Weight
		{
			get => _character.Weight;
			set
			{
				_character.Weight = value;
				RaisePropertyChanged();
			}
		}

		public string Hair
		{
			get => _character.Hair;
			set
			{
				_character.Hair = value;
				RaisePropertyChanged();
			}
		}

		public string Skin
		{
			get => _character.Skin;
			set
			{
				_character.Skin = value;
				RaisePropertyChanged();
			}
		}

		public string Eye
		{
			get => _character.Eyes;
			set
			{
				_character.Eyes = value;
				RaisePropertyChanged();
			}
		}

		public int StrengthStat
		{
			get => _character.AbilityScores[MainStatType.Strength];
			set
			{
				_character.AbilityScores[MainStatType.Strength] = value;
				RaisePropertyChanged();
			}
		}

		public int DexterityStat
		{
			get => _character.AbilityScores[MainStatType.Dexterity];
			set
			{
				_character.AbilityScores[MainStatType.Dexterity] = value;
				RaisePropertyChanged();
			}
		}

		public int ConstitutionStat
		{
			get => _character.AbilityScores[MainStatType.Constitution];
			set
			{
				_character.AbilityScores[MainStatType.Constitution] = value;
				RaisePropertyChanged();
			}
		}

		public int IntelligenceStat
		{
			get => _character.AbilityScores[MainStatType.Intelligence];
			set
			{
				_character.AbilityScores[MainStatType.Intelligence] = value;
				RaisePropertyChanged();
			}
		}

		public int WisdomStat
		{
			get => _character.AbilityScores[MainStatType.Wisdom];
			set
			{
				_character.AbilityScores[MainStatType.Wisdom] = value;
				RaisePropertyChanged();
			}
		}

		public int CharismaStat
		{
			get => _character.AbilityScores[MainStatType.Charisma];
			set
			{
				_character.AbilityScores[MainStatType.Charisma] = value;
				RaisePropertyChanged();
			}
		}
		private int _selectedSubClassId;
		public int SelectedSubClass
		{
			get => _selectedSubClassId;
			set
			{
				_selectedSubClassId = value;
				RaisePropertyChanged();
			}
		}

		#endregion
		#region Lists
		private readonly ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();

		public ObservableCollection<Skill> Skills
		{
			get
			{
				if (_skills.Count == 0)
				{
					foreach (var skill in _character.Skills)
					{
						Debug.WriteLine("Added skill to list: " + skill.Name);
						_skills.Add(skill);
					}
				}
				return _skills;
			}
		}

		private ObservableCollection<RaceViewModel> _races = new ObservableCollection<RaceViewModel>();

		public ObservableCollection<RaceViewModel> Races
		{
			get
			{
				if (_races.Count == 0)
				{
					foreach (var race in CharacterManager.RacialBonuses)
					{
						if (race.ParentRace == "")
						{
							_races.Add(new RaceViewModel(race));
						}
					}
				}
				return _races;
			}
		}


		#region Race
		private int _selectedRaceIndex = -1;

		public int SelectedRaceIndex
		{
			get => _selectedRaceIndex;
			set
			{
				_selectedRaceIndex = value;
				RaisePropertyChanged();
				RaisePropertyChanged("SelectedRace");
				RaiseAllSelectedRaceChanged();
			}
		}

		#region SelectedRace

		private RaceViewModel SelectedRace => SelectedRaceIndex >= 0 && SelectedRaceIndex < Races.Count ? Races[SelectedRaceIndex] : new RaceViewModel();


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

		private int _selectedSubRaceIndex = -1;

		public int SelectedSubRaceIndex
		{
			get => _selectedSubRaceIndex;
			set
			{
				_selectedSubRaceIndex = value;
				RaisePropertyChanged();
				RaiseAllSelectedSubraceChanged();
			}
		}

		#endregion

		private RaceViewModel SelectedSubrace => _selectedSubRaceIndex >= 0 && _selectedSubRaceIndex < Subraces.Count ? Subraces[_selectedSubRaceIndex] : new RaceViewModel();


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
					if (race.ParentRace == SelectedRace.RacialBonuses.Race)
					{
						subraces.Add(new RaceViewModel(race));
					}
				}
				return subraces;
			}
		}



		private ObservableCollection<ClassViewModel> _classes = new ObservableCollection<ClassViewModel>();

		public ObservableCollection<ClassViewModel> Classes
		{
			get
			{
				if (_classes.Count == 0)
				{
					foreach (var cl in CharacterManager.ClassBonuses)
					{
						_classes.Add(new ClassViewModel(cl));
					}
				}
				return _classes;
			}
		}

		private int _selectedClassIndex;

		public int SelectedClassIndex
		{
			get => _selectedClassIndex;
			set
			{
				_selectedClassIndex = value;
				RaisePropertyChanged();
			}
		}



		private static ObservableCollection<string> _alignments = new ObservableCollection<string>() { "Chaotic Evil", "Neutral Evil", "Lawful Evil", "Chaotic Neutral", "Neutral", "Lawful Neutral", "Chaotic Good", "Neutral Good", "Lawful Good" };

		public ObservableCollection<string> Alignments => _alignments;

		public int SelectedAlignment { get; set; } = -1;





		#endregion


		private string _subclassChoiceStatement = "Choose your subclass{default}";

		public string SubclassChoiceStatement => _subclassChoiceStatement;


		private ObservableCollection<string> _rolledAbilityScores = new ObservableCollection<string>();

		public ObservableCollection<string> RolledAbilityScores => _rolledAbilityScores;

		public void RollAbilityScores()
		{

			_rolledAbilityScores.Clear();
			for (var i = 0; i < 6; i++)
			{
				int final;
				var rolls = new List<int>
				{
					_randd6.Next(1, 7),
					_randd6.Next(1, 7),
					_randd6.Next(1, 7),
					_randd6.Next(1, 7)
				};
				rolls.Sort();
				final = rolls[1] + rolls[2] + rolls[3];
				_rolledAbilityScores.Add(final + "");


			}
		}

		#region Commands

		public ICommand EndCharacterCreation => new RelayCommand(FinishedCharacterCreation);
		
		public void FinishedCharacterCreation()
		{
			foreach (var trait in SelectedRaceTraits)
			{

				_character.Traits.AddRange(trait.GetSelectedItems());

			}
			foreach (var bonus in SelectedRaceAbilityScoreBonuses)
			{
				foreach (var bonus1 in bonus.GetSelectedItems())
				{
					_character.AbilityScores[bonus1.Stat] += bonus1.Bonus;
				}
			}

			foreach (var prof in SelectedRaceLanguageProfs)
			{
				_character.Languages.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceWeaponProfs)
			{
				_character.WeaponProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceArmorProfs)
			{
				_character.ArmorProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceToolProfs)
			{
				_character.Proficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedRaceToolProfs)
			{
				_character.Proficiencies.AddRange(prof.GetSelectedItems());
			}

			foreach (var trait in SelectedSubraceTraits)
			{

				_character.Traits.AddRange(trait.GetSelectedItems());

			}
			foreach (var bonus in SelectedSubraceAbilityScoreBonuses)
			{
				foreach (var bonus1 in bonus.GetSelectedItems())
				{
					_character.AbilityScores[bonus1.Stat] += bonus1.Bonus;
				}
			}

			foreach (var prof in SelectedSubraceLanguageProfs)
			{
				_character.Languages.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceWeaponProfs)
			{
				_character.WeaponProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceArmorProfs)
			{
				_character.ArmorProficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceToolProfs)
			{
				_character.Proficiencies.AddRange(prof.GetSelectedItems());
			}
			foreach (var prof in SelectedSubraceToolProfs)
			{
				_character.Proficiencies.AddRange(prof.GetSelectedItems());
			}
		}

		#endregion




	}
}

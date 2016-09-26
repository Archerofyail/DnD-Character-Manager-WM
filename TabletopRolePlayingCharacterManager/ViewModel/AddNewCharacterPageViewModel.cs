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
using SQLiteNetExtensionsAsync.Extensions;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	internal class AddNewCharacterPageViewModel : INotifyCollectionChanged, INotifyPropertyChanged
	{
		private readonly Random _randd6 = new Random();


		#region CharacterData
		private string _charName = "";

		public string CharName
		{
			get { return _charName; }
			set
			{
				_charName = value;
				NotifyPropertyChanged();

			}
		}

		private string _age = "10";

		public string Age
		{
			get
			{
				return _age;
			}
			set
			{
				_age = value;
				NotifyPropertyChanged();
			}
		}

		private string _height = "72 inches";

		public string Height
		{
			get
			{
				return _height;
			}
			set
			{
				_height = value;
				NotifyPropertyChanged();
			}
		}

		private string _weight = "145 lbs.";

		public string Weight
		{
			get
			{
				return _weight;
			}
			set
			{
				_weight = value;
				NotifyPropertyChanged();
			}
		}

		private string _hair = "Auburn";

		public string Hair
		{
			get
			{
				return _hair;
			}
			set
			{
				_hair = value;
				NotifyPropertyChanged();
			}
		}

		private string _skin = "Tan";

		public string Skin
		{
			get
			{
				return _skin;
			}
			set
			{
				_skin = value;
				NotifyPropertyChanged();
			}
		}

		private string _eye = "Blue";

		public string Eye
		{
			get
			{
				return _eye;
			}
			set
			{
				_eye = value;
				NotifyPropertyChanged();
			}
		}

		private int _strengthStat;

		public int StrengthStat
		{
			get { return _strengthStat; }
			set
			{
				_strengthStat = value;
				NotifyPropertyChanged();
			}
		}

		private int _dexterityStat;

		public int DexterityStat
		{
			get { return _dexterityStat; }
			set
			{
				_dexterityStat = value;
				NotifyPropertyChanged();
			}
		}

		private int _constitutionStat;

		public int ConstitutionStat
		{
			get { return _constitutionStat; }
			set
			{
				_constitutionStat = value;
				NotifyPropertyChanged();
			}
		}

		private int _intelligenceStat;

		public int IntelligenceStat
		{
			get { return _intelligenceStat; }
			set
			{
				_intelligenceStat = value;
				NotifyPropertyChanged();
			}
		}

		private int _wisdomStat;

		public int WisdomStat
		{
			get { return _wisdomStat; }
			set
			{
				_wisdomStat = value;
				NotifyPropertyChanged();
			}
		}

		private int _charismaStat;

		public int CharismaStat
		{
			get { return _charismaStat; }
			set
			{
				_charismaStat = value;
				NotifyPropertyChanged();
			}
		}
		private int _selectedSubClassId;
		public int SelectedSubClass
		{
			get { return _selectedSubClassId; }
			set
			{
				_selectedSubClassId = value;
				NotifyPropertyChanged();
			}
		}
		private int _selectedAlignment;
		public int SelectedAlignment
		{
			get { return _selectedAlignment; }
			set
			{
				_selectedAlignment = value;
				NotifyPropertyChanged();
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
					foreach (var skill in DbLoader.Skills)
					{
						Debug.WriteLine("Added skill to list: " + skill.Name);
						_skills.Add(skill);
					}
				}
				return _skills;
			}
		}

		private ObservableCollection<Proficiency> _languages = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> Languages
		{
			get
			{
				if (_languages.Count == 0)
				{
					foreach (var language in DbLoader.GetTable<Proficiency>())
					{
						if (language.Category == "Language")
						{
							_languages.Add(language);
						}
					}
				}
				return _languages;
			}
		}

		private ObservableCollection<Race> _races = new ObservableCollection<Race>();

		public ObservableCollection<Race> Races
		{
			get
			{
				if (_races.Count == 0)
				{
					foreach (var race in DbLoader.GetTable<Race>())
					{
						_races.Add(race);
					}
				}
				return _races;
			}
		}

		private int _selectedRaceId;

		public int SelectedRace
		{
			get { return _selectedRaceId; }
			set
			{
				_selectedRaceId = value;
				NotifyPropertyChanged();
			}
		}


		private ObservableCollection<Subrace> _subraces = new ObservableCollection<Subrace>();

		public ObservableCollection<Subrace> Subraces
		{
			get
			{
				if (_subraces.Count == 0)
				{
					foreach (var race in DbLoader.GetTable<Subrace>())
					{
						_subraces.Add(race);
					}

				}
				return _subraces;
			}
		}

		private int _selectedSubRaceId;

		public int SelectedSubRace
		{
			get { return _selectedSubRaceId; }
			set
			{
				_selectedSubRaceId = value;
				NotifyPropertyChanged();
			}
		}

		private ObservableCollection<Class> _classes = new ObservableCollection<Class>();

		public ObservableCollection<Class> Classes
		{
			get
			{
				if (_classes.Count == 0)
				{
					foreach (var Class in DbLoader.GetTable<Class>())
					{
						_classes.Add(Class);
					}
				}
				return _classes;
			}
		}

		private int _selectedClassId;

		public int SelectedClass
		{
			get { return _selectedClassId; }
			set
			{
				_selectedClassId = value;
				NotifyPropertyChanged();
			}
		}


		private ObservableCollection<Subclass> _subclasses = new ObservableCollection<Subclass>();

		public ObservableCollection<Subclass> Subclasses
		{
			get
			{
				if (_subclasses.Count == 0)
				{
					foreach (var subclass in DbLoader.GetTable<Subclass>())
					{
						_subclasses.Add(subclass);
					}
				}
				return _subclasses;

			}
		}
		private ObservableCollection<Alignment> _alignments = new ObservableCollection<Alignment>();

		public ObservableCollection<Alignment> Alignments
		{
			get
			{
				if (_alignments.Count != 0) return _alignments;
				foreach (var alignment in DbLoader.GetTable<Alignment>())
				{
					_alignments.Add(alignment);
				}
				return _alignments;
			}
			set
			{
				_alignments = value;
				NotifyPropertyChanged();
			}
		}

		private ObservableCollection<Proficiency> _weaponProficiencies = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> WeaponProficiencies
		{
			get
			{
				if (_weaponProficiencies.Count == 0)
				{
					var proficiencies = DbLoader.GetTable<Proficiency>();
					foreach (var prof in proficiencies)
					{
						if (prof.Category == "Weapon")
						{
							_weaponProficiencies.Add(prof);
						}
					}
				}
				return _weaponProficiencies;
			}
		}

		private ObservableCollection<Proficiency> _armorProficiencies = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> ArmorProficiencies
		{
			get
			{
				if (_weaponProficiencies.Count == 0)
				{
					var proficiencies = DbLoader.GetTable<Proficiency>();
					foreach (var prof in proficiencies)
					{
						if (prof.Category == "Armor")
						{
							_armorProficiencies.Add(prof);
						}
					}
				}
				return _armorProficiencies;
			}
		}

		private ObservableCollection<string> _characterTemplates = new ObservableCollection<string>();
		public ObservableCollection<string> CharacterTemplates
		{
			get
			{
				if (_characterTemplates.Count != 0) return _characterTemplates;
				_characterTemplates.Add("Fifth Edition Character");
				_characterTemplates.Add("Generic Character");
				foreach (var characterTemplate in DbLoader.CharacterTemplates)
				{
					_characterTemplates.Add(characterTemplate.TemplateName);
				}
				return _characterTemplates;
			}
		}

		#endregion


		private string _subclassChoiceStatement = "Choose your subclass{default}";

		public string SubclassChoiceStatement
		{
			get { return _subclassChoiceStatement; }
		}




		private ObservableCollection<string> _rolledAbilityScores = new ObservableCollection<string>();

		public ObservableCollection<string> RolledAbilityScores
		{
			get { return _rolledAbilityScores; }
		}

		public void RollAbilityScores()
		{

			_rolledAbilityScores.Clear();
			for (int i = 0; i < 6; i++)
			{
				int final;
				List<int> rolls = new List<int>()
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

		#region UIControls

		private bool _isChoosingTemplates = true;
		private bool _didSelect5ECharacter;
		public Visibility IsChoosingTemplate => _isChoosingTemplates ? Visibility.Visible : Visibility.Collapsed;
		public Visibility Show5ECharacterCreator => _didSelect5ECharacter ? Visibility.Visible : Visibility.Collapsed;
		public Visibility ShowGeneralCharacterCreator => !_didSelect5ECharacter ? Visibility.Visible : Visibility.Collapsed;

		public Visibility RolledScoresExist
		{
			get
			{
				if (_rolledAbilityScores.Count > 0)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
		}
		#endregion

		public async void CreateCharacter()
		{
			Debug.WriteLine("Adding new character to database");
			var races = DbLoader.Races;
			var subraces = DbLoader.Subraces;
			var classes = DbLoader.Classes;
			var subclasses = DbLoader.Subclasses;
			var selRace = races.Find(x => x.id == Races[_selectedRaceId].id);
			var selClass = classes.Find(x => x.id == Races[_selectedClassId].id);
			var selsubclass = subclasses.Find(x => x.id == Races[_selectedSubClassId].id);
			var selsubrace = subraces.Find(x => x.id == Races[_selectedSubRaceId].id);
			var selAlignment = DbLoader.Alignments.Find(x => x.id == Alignments[_selectedAlignment].id);
			Character5E character = new Character5E()
			{
				Age = int.Parse(_age),
				Height = _height,
				SkinColor = _skin,
				EyeColor = _eye,
				HairColor = _hair,
				Weight = _weight,
				Name = CharName,

			};
			character.MainStats.Add(MainStat.Strength, _strengthStat);
			character.MainStats.Add(MainStat.Dexterity, _dexterityStat);
			character.MainStats.Add(MainStat.Constitution, _constitutionStat);
			character.MainStats.Add(MainStat.Intelligence, _intelligenceStat);
			character.MainStats.Add(MainStat.Wisdom, _wisdomStat);
			character.MainStats.Add(MainStat.Charisma, _charismaStat);


			await DbLoader.DbConnection.InsertAsync(character);
			Debug.WriteLine("Creating onetomany relationships with races and classes");
			if (selRace != null)
			{
				selRace.Characters.Add(character);
				await DbLoader.DbConnection.UpdateWithChildrenAsync(selRace);
			}
			if (selsubrace != null)
			{
				selsubrace.Characters.Add(character);
				await DbLoader.DbConnection.UpdateWithChildrenAsync(selsubrace);
			}
			if (selClass != null)
			{
				selClass.Characters.Add(character);
				await DbLoader.DbConnection.UpdateWithChildrenAsync(selClass);
			}
			if (selsubclass != null)
			{
				selsubclass.Characters.Add(character);
				await DbLoader.DbConnection.UpdateWithChildrenAsync(selsubclass);
			}
			if (selAlignment != null)
			{
				selAlignment.Characters.Add(character);
				await DbLoader.DbConnection.UpdateWithChildrenAsync(selAlignment);
			}
			if (character.Race == selRace)
			{
				Debug.WriteLine("It actually works");
				Debug.WriteLine("IDs for stuff are:\nRace: " + character.Race.Name + ", " + character.RaceId + "\nClass: " + character.Class.Name + ", " + character.ClassId);
			}
			else
			{
				Debug.WriteLine("It didn't work");
			}
		}

		public AddNewCharacterPageViewModel()
		{
		}


		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged([CallerMemberName]string senderName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(senderName));
			}
		}

		public void CharacterTemplateChosen(int selectedIndex)
		{
			
			_isChoosingTemplates = false;
			//need to minus by two for the templated in the database
			if (selectedIndex > 1)
			{
				selectedIndex -= 2;
				var selectedTemplate = DbLoader.CharacterTemplates[selectedIndex];

			}
			else if (selectedIndex == 0)
			{
				_didSelect5ECharacter = true;
				
			}
			else
			{
				_didSelect5ECharacter = false;
			}

		}


	}
}

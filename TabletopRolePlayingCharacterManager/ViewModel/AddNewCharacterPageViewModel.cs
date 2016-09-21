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
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	internal class AddNewCharacterPageViewModel : INotifyCollectionChanged, INotifyPropertyChanged
	{
		private readonly Random randd6 = new Random();

		private string charName = "";

		public string CharName
		{
			get { return charName; }
			set
			{
				charName = value;
				NotifyPropertyChanged();

			}
		}

		private string age = "10";

		public string Age
		{
			get
			{
				return age;
			}
			set
			{
				age = value;
				NotifyPropertyChanged();
			}
		}

		private string height = "72 inches";

		public string Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
				NotifyPropertyChanged();
			}
		}

		private string weight = "145 lbs.";

		public string Weight
		{
			get
			{
				return weight;
			}
			set
			{
				weight = value;
				NotifyPropertyChanged();
			}
		}

		private string hair = "Auburn";

		public string Hair
		{
			get
			{
				return hair;
			}
			set
			{
				hair = value;
				NotifyPropertyChanged();
			}
		}

		private string skin = "Tan";

		public string Skin
		{
			get
			{
				return skin;
			}
			set
			{
				skin = value;
				NotifyPropertyChanged();
			}
		}

		private string eye = "Blue";

		public string Eye
		{
			get
			{
				return eye;
			}
			set
			{
				eye = value;
				NotifyPropertyChanged();
			}
		}

		private int strengthStat = 0;

		public int StrengthStat
		{
			get { return strengthStat; }
			set
			{
				strengthStat = value;
				NotifyPropertyChanged();
			}
		}

		private int dexterityStat = 0;

		public int DexterityStat
		{
			get { return dexterityStat; }
			set
			{
				dexterityStat = value;
				NotifyPropertyChanged();
			}
		}

		private int constitutionStat = 0;

		public int ConstitutionStat
		{
			get { return constitutionStat; }
			set
			{
				constitutionStat = value;
				NotifyPropertyChanged();
			}
		}

		private int intelligenceStat = 0;

		public int IntelligenceStat
		{
			get { return intelligenceStat; }
			set
			{
				intelligenceStat = value;
				NotifyPropertyChanged();
			}
		}

		private int wisdomStat = 0;

		public int WisdomStat
		{
			get { return wisdomStat; }
			set
			{
				wisdomStat = value;
				NotifyPropertyChanged();
			}
		}

		private int charismaStat = 0;

		public int CharismaStat
		{
			get { return charismaStat; }
			set
			{
				charismaStat = value;
				NotifyPropertyChanged();
			}
		}

		private ObservableCollection<Skill> skills = new ObservableCollection<Skill>();

		public ObservableCollection<Skill> Skills
		{
			get { return skills; }
		}

		private ObservableCollection<Proficiency> languages = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> Languages
		{
			get
			{
				if (languages.Count == 0)
				{
					foreach (var language in DBLoader.GetTable<Proficiency>())
					{
						if (language.Category == "Language")
						{
							languages.Add(language);
						}
					}
				}
				return languages;
			}
		}

		private ObservableCollection<Race> races = new ObservableCollection<Race>();

		public ObservableCollection<Race> Races
		{
			get
			{
				if (races.Count == 0)
				{
					foreach (var race in DBLoader.GetTable<Race>())
					{
						races.Add(race);
					}
				}
				return races;
			}
		}

		private int selectedRace_id = 0;

		public int SelectedRace_id
		{
			get { return selectedRace_id; }
			set
			{
				selectedRace_id = value;
				NotifyPropertyChanged();
			}
		}


		private ObservableCollection<Subrace> subraces = new ObservableCollection<Subrace>();

		public ObservableCollection<Subrace> Subraces
		{
			get
			{
				if (subraces.Count == 0)
				{
					foreach (var race in DBLoader.GetTable<Subrace>())
					{
						subraces.Add(race);
					}
					
				}
				return subraces;
			}
		}

		private int selectedSubRace_id = 0;

		public int SelectedSubRace_id
		{
			get { return selectedSubRace_id; }
			set
			{
				selectedSubRace_id = value;
				NotifyPropertyChanged();
			}
		}

		private ObservableCollection<Class> classes = new ObservableCollection<Class>();

		public ObservableCollection<Class> Classes
		{
			get
			{
				if (classes.Count == 0)
				{
					foreach (var Class in DBLoader.GetTable<Class>())
					{
						classes.Add(Class);
					}
				}
				return classes;
			}
		}

		private int selectedClass_id = 0;

		public int SelectedClass_Id
		{
			get { return selectedClass_id; }
			set
			{
				selectedClass_id = value;
				NotifyPropertyChanged();
			}
		}


		private ObservableCollection<Subclass> subclasses = new ObservableCollection<Subclass>();

		public ObservableCollection<Subclass> Subclasses
		{
			get
			{
				if (subclasses.Count == 0)
				{
					foreach (var subclass in DBLoader.GetTable<Subclass>())
					{
						subclasses.Add(subclass);
					}
				}
				return subclasses;

			}
		}

		private int selectedSubClass_Id = 0;
		public int SelectedSubClass_Id
		{
			get { return selectedSubClass_Id; }
			set
			{
				selectedSubClass_Id = value;
				NotifyPropertyChanged();
			}
		}

		private string subclassChoiceStatement = "Choose your subclass{default}";

		public string SubclassChoiceStatement
		{
			get { return subclassChoiceStatement; }
		}

		private ObservableCollection<Alignment> alignments = new ObservableCollection<Alignment>();

		public ObservableCollection<Alignment> Alignments
		{
			get
			{
				if (alignments.Count == 0)
				{
					foreach (var alignment in DBLoader.GetTable<Alignment>())
					{
						alignments.Add(alignment);
					}
				}
				return alignments;
			}
			set
			{
				alignments = value;
				NotifyPropertyChanged();
			}
		}

		private ObservableCollection<Proficiency> weaponProficiencies = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> WeaponProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					var proficiencies = DBLoader.GetTable<Proficiency>();
					foreach (var prof in proficiencies)
					{
						if (prof.Category == "Weapon")
						{
							weaponProficiencies.Add(prof);
						}
					}
				}
				return weaponProficiencies;
			}
		}

		private ObservableCollection<Proficiency> armorProficiencies = new ObservableCollection<Proficiency>();

		public ObservableCollection<Proficiency> ArmorProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					var proficiencies = DBLoader.GetTable<Proficiency>();
					foreach (var prof in proficiencies)
					{
						if (prof.Category == "Armor")
						{
							armorProficiencies.Add(prof);
						}
					}
				}
				return armorProficiencies;
			}
		}

		private ObservableCollection<string> rolledAbilityScores = new ObservableCollection<string>();

		public ObservableCollection<string> RolledAbilityScores
		{
			get { return rolledAbilityScores; }
		}

		public void RollAbilityScores()
		{
			ItemSelectionStore.LoadWeaponList();
			rolledAbilityScores.Clear();
			for (int i = 0; i < 6; i++)
			{
				int final;
				List<int> rolls = new List<int>()
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


		public async void CreateCharacter()
		{
			Debug.WriteLine("Adding new character to database");
			var races = DBLoader.Races;
			var subraces = DBLoader.Subraces;
			var classes = DBLoader.Classes;
			var subclasses = DBLoader.Subclasses;
			var selRace = races.Find(x => x.id == Races[selectedRace_id].id);
			var selClass = classes.Find(x => x.id == Races[selectedClass_id].id);
			var selsubclass = subclasses.Find(x => x.id == Races[selectedSubClass_Id].id);
			var selsubrace = subraces.Find(x => x.id == Races[selectedSubRace_id].id);
			Character5E character = new Character5E()
			{
				Age = int.Parse(age),
				Height = height,
				SkinColor = skin,
				EyeColor = eye,
				HairColor = hair,
				Weight = weight,
				Name = CharName,

			};
			character.abilityModifiers.Add(MainStat.Strength, strengthStat);
			character.abilityModifiers.Add(MainStat.Dexterity, dexterityStat);
			character.abilityModifiers.Add(MainStat.Constitution, constitutionStat);
			character.abilityModifiers.Add(MainStat.Intelligence, intelligenceStat);
			character.abilityModifiers.Add(MainStat.Wisdom, wisdomStat);
			character.abilityModifiers.Add(MainStat.Charisma, charismaStat);

			await DBLoader.dbConnection.InsertAsync(character);
			Debug.WriteLine("Creating onetomany relationships with races and classes");
			if (selRace != null)
			{
				selRace.Characters.Add(character);
				await DBLoader.dbConnection.UpdateWithChildrenAsync(selRace);
			}
			if (selsubrace != null)
			{
				selsubrace.Characters.Add(character);
				await DBLoader.dbConnection.UpdateWithChildrenAsync(selsubrace);
			}
			if (selClass != null)
			{
				selClass.Characters.Add(character);
				await DBLoader.dbConnection.UpdateWithChildrenAsync(selClass);
			}
			if (selsubclass != null)
			{
				selsubclass.Characters.Add(character);
				await DBLoader.dbConnection.UpdateWithChildrenAsync(selsubclass);
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
	}
}

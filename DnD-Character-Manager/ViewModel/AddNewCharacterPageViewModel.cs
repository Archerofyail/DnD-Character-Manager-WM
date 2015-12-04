using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using DnD_Character_Manager.Model;
using DnD_Character_Manager.Types;

namespace DnD_Character_Manager.ViewModel
{
	internal class AddNewCharacterPageViewModel : INotifyCollectionChanged, INotifyPropertyChanged
	{

		private Random randd6 = new Random();

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

		private string age = "";

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

		private string height = "";

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

		private string weight = "";

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

		private string hair = "";

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

		private string skin = "";

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

		private string eye = "";

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
				IntelligenceStat = value;
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

		private ObservableCollection<string> languages = new ObservableCollection<string>();

		public ObservableCollection<string> Languages
		{
			get
			{
				if (languages.Count == 0)
				{
					foreach (var language in CharacterTraitSelectionStore.Languages)
					{
						languages.Add(language);
					}
				}
				return languages;
			}
		}

		private ObservableCollection<string> races = new ObservableCollection<string>();

		public ObservableCollection<string> Races
		{
			get
			{
				if (races.Count == 0)
				{
					foreach (var race in CharacterTraitSelectionStore.Races)
					{
						races.Add(race);
					}
				}
				return races;
			}
		}

		private int selectedRace = -1;

		public int SelectedRace
		{
			get { return selectedRace; }
			set
			{
				selectedRace = value;
				NotifyPropertyChanged();
			}
		}


		private ObservableCollection<string> subraces = new ObservableCollection<string>();

		public ObservableCollection<string> Subraces
		{
			get
			{
				if (subraces.Count == 0)
				{
					subraces.Add("default subrace");
				}
				return subraces;
			}
		}

		private int selectedSubRace = -1;

		public int SelectedSubRace
		{
			get { return selectedSubRace; }
			set
			{
				selectedSubRace = value;
				NotifyPropertyChanged();
			}
		}

		private ObservableCollection<string> classes = new ObservableCollection<string>();

		public ObservableCollection<string> Classes
		{
			get
			{
				if (classes.Count == 0)
				{
					foreach (var race in CharacterTraitSelectionStore.Classes)
					{
						classes.Add(race);
					}
				}
				return classes;
			}
		}

		private int selectedClass = -1;

		public int SelectedClass
		{
			get { return selectedClass; }
			set
			{
				selectedClass = value;
				NotifyPropertyChanged();
			}
		}


		private ObservableCollection<string> subclasses = new ObservableCollection<string>();

		public ObservableCollection<string> Subclasses
		{
			get
			{
				if (subclasses.Count == 0)
				{
					subclasses.Add("default subclass");
				}
				return subclasses;

			}
		}

		private int selectedSubClass = -1;
		public int SelectedSubClass
		{
			get { return selectedSubClass; }
			set
			{
				selectedSubClass = value;
				NotifyPropertyChanged();
			}
		}

		private string subclassChoiceStatement = "Choose your subclass{default}";

		public string SubclassChoiceStatement
		{
			get { return subclassChoiceStatement; }
		}



		private ObservableCollection<string> weaponProficiencies = new ObservableCollection<string>();

		public ObservableCollection<string> WeaponProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					foreach (var weapon in ItemSelectionStore.weaponProfList)
					{
						weaponProficiencies.Add(weapon);
					}
				}
				return weaponProficiencies;
			}
		}

		private ObservableCollection<string> armorProficiencies = new ObservableCollection<string>();

		public ObservableCollection<string> ArmorProficiencies
		{
			get
			{
				if (armorProficiencies.Count == 0)
				{
					foreach (var weapon in ItemSelectionStore.armorProfList)
					{
						armorProficiencies.Add(weapon);
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


		public void CreateCharacter()
		{
			CharacterModel5E character = new CharacterModel5E()
			{
				Age = int.Parse(age),
				Height = height,
				SkinColor = skin,
				EyeColor = eye,
				HairColor = hair,
				Weight = weight,
				Name = CharName,
				Race = Races[SelectedRace],
				Subrace = Subraces[SelectedSubClass],
				Class = Classes[selectedClass],
				SubClass = Subclasses[SelectedSubClass]

			};
			character.abilityModifiers.Add(MainStat.Strength, strengthStat);
			character.abilityModifiers.Add(MainStat.Dexterity, dexterityStat);
			character.abilityModifiers.Add(MainStat.Constitution, constitutionStat);
			character.abilityModifiers.Add(MainStat.Intelligence, intelligenceStat);
			character.abilityModifiers.Add(MainStat.Wisdom, wisdomStat);
			character.abilityModifiers.Add(MainStat.Charisma, charismaStat);
		}

		public AddNewCharacterPageViewModel()
		{
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
			skills.Add(new Skill("Athletics", MainStat.Strength, 14, 3, true));
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

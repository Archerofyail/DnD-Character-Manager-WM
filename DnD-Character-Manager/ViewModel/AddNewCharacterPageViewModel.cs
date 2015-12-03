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

		private ObservableCollection<Skill> skills = new ObservableCollection<Skill>();

		public ObservableCollection<Skill> Skills
		{
			get { return skills; }
		}

		private ObservableCollection<CharacterTrait> languages = new ObservableCollection<CharacterTrait>();

		public ObservableCollection<CharacterTrait> Languages
		{
			get
			{
				if (CharacterTraitSelectionStore.Languages.Count > 0)
				{
					languages = new ObservableCollection<CharacterTrait>(CharacterTraitSelectionStore.Languages);
				}
				if (languages.Count == 0)
				{
					
					languages.Add(new CharacterTrait("Common"));
					languages.Add(new CharacterTrait("Dwarven"));
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

		private ObservableCollection<CharacterTrait> weaponProficiencies = new ObservableCollection<CharacterTrait>();

		public ObservableCollection<CharacterTrait> WeaponProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					weaponProficiencies.Add(new CharacterTrait("Simple"));
					weaponProficiencies.Add(new CharacterTrait("Warhammer"));
				}
				return weaponProficiencies;
			}
		}

		private ObservableCollection<CharacterTrait> armorProficiencies = new ObservableCollection<CharacterTrait>();

		public ObservableCollection<CharacterTrait> ArmorProficiencies
		{
			get
			{
				if (armorProficiencies.Count == 0)
				{
					armorProficiencies.Add(new CharacterTrait("Light"));
					armorProficiencies.Add(new CharacterTrait("Medium"));
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

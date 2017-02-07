using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	//TODO: Save some values for the view to a json file (like if the global list checkboxes are checked)
	//If the CharacterManager's current character is empty when loading this page, assume a new character was created.
	public class CharacterSheetViewModel : ViewModelBase
	{
		Character5E character;

		public CharacterSheetViewModel()
		{
			character = CharacterManager.CurrentCharacter;
			if (character == null)
			{
				character = CharacterManager.GetNewChar();
			}
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

		#region CombatStats

		public string ArmorClass
		{
			get { return character.ArmorClass.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					character.ArmorClass = result;
				}
			}
		}

		public string Initiative
		{
			get { return character.Initiative.ToString(); }
		}

		public string Speed
		{
			get { return character.Speed.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					if (result % 5 == 0)
					{
						character.Speed = result;
					}
				}
			}
		}

		public string CurrentHealth
		{
			get { return character.CurrHP.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					character.CurrHP = result;
				}
			}
		}

		public string MaxHealth
		{
			get { return character.MaxHP.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					character.MaxHP = result;
				}
			}
		}
		public string TemporaryHealth
		{
			get { return character.TempHP.ToString(); }
			set
			{
				var result = 0;
				if (int.TryParse(value, out result))
				{
					character.TempHP = result;
				}
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
		public bool StrIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStat.Strength]; }
			set
			{
				character.AbilityScoreProficiencies[MainStat.Strength] = value;
				RaisePropertyChanged();
			}
		}
		public bool DexIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStat.Dexterity]; }
			set
			{
				character.AbilityScoreProficiencies[MainStat.Dexterity] = value;
				RaisePropertyChanged();
			}
		}
		public bool ConIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStat.Constitution]; }
			set
			{
				character.AbilityScoreProficiencies[MainStat.Constitution] = value;
				RaisePropertyChanged();
			}
		}
		public bool IntIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStat.Intelligence]; }
			set
			{
				character.AbilityScoreProficiencies[MainStat.Intelligence] = value;
				RaisePropertyChanged();
			}
		}
		public bool WisIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStat.Wisdom]; }
			set
			{
				character.AbilityScoreProficiencies[MainStat.Wisdom] = value;
				RaisePropertyChanged();
			}
		}
		public bool ChaIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStat.Charisma]; }
			set
			{
				character.AbilityScoreProficiencies[MainStat.Charisma] = value;
				RaisePropertyChanged();
			}
		}
		#endregion
		#endregion

		#region Personality

		public string PersonalityTraits
		{
			get { return character.PersonalityTraits; }
			set { character.PersonalityTraits = value; }
		}
		public string Ideals
		{
			get { return character.Ideals; }
			set { character.Ideals = value; }
		}
		public string Bonds
		{
			get { return character.Bonds; }
			set { character.Bonds = value; }
		}
		public string Flaws
		{
			get { return character.Flaws; }
			set { character.Flaws = value; }
		}
		#endregion

		#region Lists
		private ObservableCollection<SkillViewModel> skills = new ObservableCollection<SkillViewModel>();
		public ObservableCollection<SkillViewModel> Skills
		{
			get
			{
				if (skills.Count == 0)
				{
					foreach (var skill in character.Skills)
					{
						skills.Add(new SkillViewModel(skill));
					}
					RaisePropertyChanged();
				}
				return skills;
			}
		}

		private ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();

		public ObservableCollection<ItemViewModel> Items
		{
			get
			{
				if (items.Count == 0)
				{
					foreach (var item in character.Inventory)
					{
						items.Add(new ItemViewModel(item));
					}
				}
				return items;

			}
		}

		private ObservableCollection<SpellViewModel> spells = new ObservableCollection<SpellViewModel>();

		public ObservableCollection<SpellViewModel> Spells
		{
			get
			{
				if (spells.Count == 0)
				{
					foreach (var item in character.Inventory)
					{
						spells.Add(new SpellViewModel());
					}
				}
				return spells;

			}
		}

		#region GlobalListControl

		private bool addSpellToGlobalList;

		public bool AddSpellToGlobalList
		{
			get { return addSpellToGlobalList;}
			set
			{
				addSpellToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		private bool addItemToGlobalList;

		public bool AddItemToGlobalList
		{
			get { return addItemToGlobalList; }
			set
			{
				addItemToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		private bool addWeaponToGlobalList;

		public bool AddWeaponToGlobalList
		{
			get { return addWeaponToGlobalList; }
			set
			{
				addWeaponToGlobalList = value;
				RaisePropertyChanged();
			}
		}
		#endregion
		#endregion

		#region Commands


		public ICommand SaveCharacter { get { return new RelayCommand(SaveCharacterExecute, CanRunCommand); } }
		public ICommand DeleteItem => new RelayCommand<Item>(DeleteItemExecute);
		public ICommand DeleteWeapon => new RelayCommand<Weapon>(DeleteWeaponExecute);

		#region CommandFunctions
		void SaveCharacterExecute()
		{
			CharacterManager.SaveCurrentCharacter();
		}

		bool CanRunCommand()
		{
			return true;
		}

		void DeleteItemExecute(Item param)
		{
			character.Inventory.Remove(param as Item);
		}

		void DeleteWeaponExecute(Weapon param)
		{
			character.Weapons.Remove(param);
		}

		//TODO: create new spell/item/weapon, based on values in flyouts that I'll make later
		//Then, if it's added to the global list, add it to the global list
		//Then, add it to the characters list, then create a new viewmodel based on it and add that to the observablecollection. 
		//Then, finally, raise property changed the collection
		void AddNewItem()
		{
			if (AddItemToGlobalList)
			{
				
			}
		}

		void AddNewSpell()
		{
			if (AddSpellToGlobalList)
			{
				
			}
		}

		void AddNewWeapon()
		{
			if (AddWeaponToGlobalList)
			{
				
			}
		}
		#endregion
		#endregion
	}
}

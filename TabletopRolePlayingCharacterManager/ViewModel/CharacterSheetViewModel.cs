using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
		private static ObservableCollection<string> alignments = new ObservableCollection<string>() {"Chaotic Evil", "Neutral Evil", "Lawful Evil", "Chaotic Neutral", "Neutral", "Lawful Neutral", "Chaotic Good", "Neutral Good", "Lawful Good"};
		public CharacterSheetViewModel()
		{
			character = CharacterManager.CurrentCharacter;
			if (character == null)
			{
				character = CharacterManager.GetNewChar();
			}
		}

		#region General

		public string Notes
		{
			get { return character.Notes; }
			set
			{
				character.Notes = value;
				RaisePropertyChanged();
			}
		}

		public string Campaign
		{
			get { return character.Campaign; }
			set
			{
				character.Campaign = value;
				RaisePropertyChanged();
			}
		}

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

		public ObservableCollection<string> Alignments
		{
			get { return alignments; }
		}

		private int selectedAlignment = -1;

		public int SelectedAlignment
		{
			get { return selectedAlignment; }
			set
			{
				if (selectedAlignment < alignments.Count)
				{
					selectedAlignment = value;
					character.Alignment = alignments[value];
					RaisePropertyChanged();
				}
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

		public bool[] DeathSaveSuccesses
		{
			get { return character.DeathSaveSuccesses; }
			set { character.DeathSaveSuccesses = value; }
		}
		public bool[] DeathSaveFails
		{
			get { return character.DeathSaveFails; }
			set { character.DeathSaveFails = value; }
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
			get { return character.AbilityScores[MainStatType.Strength].ToString(); }
			set
			{
				var result = character.AbilityScores[MainStatType.Strength];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStatType.Strength] = result;
				}
				character.CalculateAbilityModifiers();
				character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("StrengthMod");
				RaisePropertyChanged();
			}
		}
		public string Dexterity
		{
			get { return character.AbilityScores[MainStatType.Dexterity].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStatType.Dexterity];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStatType.Dexterity] = result;
				}
				character.CalculateAbilityModifiers();
				character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("DexterityMod");
				RaisePropertyChanged();
			}
		}
		public string Constitution
		{
			get { return character.AbilityScores[MainStatType.Constitution].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStatType.Constitution];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStatType.Constitution] = result;
				}
				character.CalculateAbilityModifiers();
				character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("ConstitutionMod");
				RaisePropertyChanged();
			}
		}
		public string Intelligence
		{
			get { return character.AbilityScores[MainStatType.Intelligence].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStatType.Intelligence];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStatType.Intelligence] = result;
				}
				character.CalculateAbilityModifiers();
				character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("IntelligenceMod");
				RaisePropertyChanged();
			}
		}
		public string Wisdom
		{
			get { return character.AbilityScores[MainStatType.Wisdom].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStatType.Wisdom];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStatType.Wisdom] = result;
				}
				character.CalculateAbilityModifiers();
				character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("WisdomMod");
				RaisePropertyChanged();
			}
		}
		public string Charisma
		{
			get { return character.AbilityScores[MainStatType.Charisma].ToString(); }
			set
			{

				var result = character.AbilityScores[MainStatType.Charisma];
				if (int.TryParse(value, out result))
				{
					character.AbilityScores[MainStatType.Charisma] = result;
				}
				character.CalculateAbilityModifiers();
				character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("CharismaMod");
				RaisePropertyChanged();
			}
		}

		#region AbilityModifiers

		public string StrengthMod
		{
			get { return "+" + character.abilityModifiers[MainStatType.Strength].ToString(); }
		}
		public string DexterityMod
		{
			get { return "+" + character.abilityModifiers[MainStatType.Dexterity].ToString(); }
		}
		public string ConstitutionMod
		{
			get { return "+" + character.abilityModifiers[MainStatType.Constitution].ToString(); }
		}
		public string IntelligenceMod
		{
			get { return "+" + character.abilityModifiers[MainStatType.Intelligence].ToString(); }
		}
		public string WisdomMod
		{
			get { return "+" + character.abilityModifiers[MainStatType.Wisdom].ToString(); }
		}
		public string CharismaMod
		{
			get { return "+" + character.abilityModifiers[MainStatType.Charisma].ToString(); }
		}
		#endregion

		#region AbilityProficiencies
		public bool StrIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStatType.Strength]; }
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Strength] = value;
				RaisePropertyChanged();
			}
		}
		public bool DexIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStatType.Dexterity]; }
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Dexterity] = value;
				RaisePropertyChanged();
			}
		}
		public bool ConIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStatType.Constitution]; }
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Constitution] = value;
				RaisePropertyChanged();
			}
		}
		public bool IntIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStatType.Intelligence]; }
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Intelligence] = value;
				RaisePropertyChanged();
			}
		}
		public bool WisIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStatType.Wisdom]; }
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Wisdom] = value;
				RaisePropertyChanged();
			}
		}
		public bool ChaIsProficient
		{
			get { return character.AbilityScoreProficiencies[MainStatType.Charisma]; }
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Charisma] = value;
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

		public string Backstory
		{
			get { return character.Backstory; }
			set
			{
				character.Backstory = value;
				RaisePropertyChanged();
			}
		}

		public string God
		{
			get { return character.God; }
			set
			{
				character.God = value;
				RaisePropertyChanged();
			}
		}

		public string Relationships
		{
			get { return character.RelationshipsAndAllies; }
			set
			{
				character.RelationshipsAndAllies = value;
				RaisePropertyChanged();
			}
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
					foreach (var item in character.Spells)
					{
						spells.Add(new SpellViewModel(item));
					}
				}
				return spells;

			}
		}
		private ObservableCollection<WeaponViewModel> weapons = new ObservableCollection<WeaponViewModel>();

		public ObservableCollection<WeaponViewModel> Weapons
		{
			get
			{
				if (weapons.Count == 0)
				{
					foreach (var item in character.Weapons)
					{
						weapons.Add(new WeaponViewModel(item));
					}
				}
				return weapons;

			}
		}

		private ObservableCollection<TraitViewModel> traits = new ObservableCollection<TraitViewModel>();

		public ObservableCollection<TraitViewModel> CharTraits
		{
			get
			{
				if (traits.Count == 0)
				{
					foreach (var trait in character.Traits)
					{
						traits.Add(new TraitViewModel(trait));
					}

				}
				return traits;
			}
		}
		#region GlobalListControl

		private bool addSpellToGlobalList;

		public bool AddSpellToGlobalList
		{
			get { return addSpellToGlobalList; }
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

		#region StuffToAdd
		#region Item

		private string newItemName = "";
		public string NewItemName
		{
			get { return newItemName; }
			set
			{
				newItemName = value;
				RaisePropertyChanged();
			}
		}
		private string newItemDesc = "";
		public string NewItemDesc
		{
			get { return newItemDesc; }
			set
			{
				newItemDesc = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region  Weapon
		private string newWepName = "";
		public string NewWepName
		{
			get { return newWepName; }
			set
			{
				newWepName = value;
				RaisePropertyChanged();
			}
		}
		private Damage newWepDamage = new Damage();

		public string NewWepDamage
		{
			get
			{
				return newWepDamage.ToString();

			}
			set
			{
				var matches = Regex.Match(value, @"(\d)(d\d{1,2})");
				Debug.WriteLine("Matches: " + matches.Value + ". groups count is " + matches.Groups.Count);
				if (matches.Success && matches.Groups.Count >= 3)
				{
					var numDice = 0;
					DieType DieType = DieType.D4;
					if (int.TryParse(matches.Groups[1].Value, out numDice))
					{
						if (Enum.TryParse(matches.Groups[2].Value, out DieType))
						{
							newWepDamage.Dice.Clear();
							newWepDamage.Dice.Add(DieType, numDice);
						}
					}
				}
			}
		}
		private string newWepDesc = "";
		public string NewWepDesc
		{
			get { return newWepDesc; }
			set
			{
				newWepDesc = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Spell

		private string newSpellName;

		public string NewSpellName
		{
			get { return newSpellName; }
			set
			{
				newSpellName = value;
				RaisePropertyChanged();
			}
		}

		private int newSpellLevel = 0;
		public string NewSpellLevel
		{
			get { return newSpellLevel.ToString(); }
			set
			{
				int result = 0;
				if (int.TryParse(value, out result))
				{
					newSpellLevel = result;
					RaisePropertyChanged();
				}
			}
		}

		private string newSpellDesc;

		public string NewSpellDesc
		{
			get { return newSpellDesc; }
			set
			{
				newSpellDesc = value;
				RaisePropertyChanged();
			}
		}

		private bool nSpellHasV = false;

		public bool NSpellHasV
		{
			get { return nSpellHasV; }
			set
			{
				nSpellHasV = value;
				RaisePropertyChanged();
			}
		}

		private bool nSpellHasS = false;

		public bool NSpellHasS
		{
			get { return nSpellHasS; }
			set
			{
				nSpellHasS = value;
				RaisePropertyChanged();
			}
		}


		private string nSpellMat = "";

		public string NSpellMat
		{
			get { return nSpellMat; }
			set
			{
				nSpellMat = value;
				RaisePropertyChanged();
			}
		}


		#endregion

		#region Traits

		private string newTraitDesc = "";

		public string NewTraitDesc
		{
			get { return newTraitDesc; }
			set
			{
				newTraitDesc = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#endregion


		#region Commands


		public ICommand SaveCharacter { get { return new RelayCommand(SaveCharacterExecute, CanRunCommand); } }
		public ICommand DeleteItem => new RelayCommand<Item>(DeleteItemExecute);
		public ICommand DeleteWeapon => new RelayCommand<Weapon>(DeleteWeaponExecute);
		public ICommand AddNewItem => new RelayCommand(AddNewItemExecute, CanRunCommand);
		public ICommand AddNewSpell => new RelayCommand(AddNewSpellExecute, CanRunCommand);
		public ICommand AddNewWeapon => new RelayCommand(AddNewWeaponExecute, CanRunCommand);
		public ICommand AddNewCharTrait => new RelayCommand(AddNewCharTraitExecute);

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
		void AddNewItemExecute()
		{
			if (AddItemToGlobalList)
			{

			}
		}

		async void AddNewSpellExecute()
		{
			Spell newSpell = new Spell();
			newSpell.Name = newSpellName;
			newSpell.Description = newSpellDesc;
			newSpell.Level = newSpellLevel;
			if (AddSpellToGlobalList)
			{
				CharacterManager.AllSpells.Add(newSpell);
			}
		}

		async void AddNewWeaponExecute()
		{
			var newWep = new Weapon();
			newWep.Name = newWepName;
			newWep.Damage = newWepDamage;
			newWep.Description = newWepDesc;
			if (AddWeaponToGlobalList)
			{
				CharacterManager.AllWeapons.Add(newWep);
				CharacterManager.SaveWeapons();
			}
			character.Weapons.Add(newWep);
			Weapons.Add(new WeaponViewModel(newWep));
			CharacterManager.SaveCurrentCharacter();
		}

		async void AddNewCharTraitExecute()
		{
			var newTrait = new Trait(newTraitDesc);
			newTraitDesc = "";
			character.Traits.Add(newTrait);
			CharTraits.Add(new TraitViewModel(newTrait));
			CharacterManager.SaveCurrentCharacter();
		}
		#endregion
		#endregion

		#region MiscFunctions

		void RaiseSkillsChanged()
		{
			foreach (var skillViewModel in Skills)
			{
				skillViewModel.RaiseAllPropertiesChanged();
			}
		}
#endregion
	}
}

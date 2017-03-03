﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	//TODO: Save some values for the view to a json file (like if the global list checkboxes are checked)
	//If the CharacterManager's current character is empty when loading this page, assume a new character was created.
	public class CharacterSheetViewModel : ViewModelBase
	{

		Character5E character;
		private static ObservableCollection<string> _alignments = new ObservableCollection<string>() { "Chaotic Evil", "Neutral Evil", "Lawful Evil", "Chaotic Neutral", "Neutral", "Lawful Neutral", "Chaotic Good", "Neutral Good", "Lawful Good" };
		public CharacterSheetViewModel()
		{
			character = CharacterManager.CurrentCharacter;
			if (character == null)
			{
				character = CharacterManager.GetNewChar();
			}
		}

		#region General
		private ObservableCollection<ProficiencyViewModel> languages = new ObservableCollection<ProficiencyViewModel>();
		public ObservableCollection<ProficiencyViewModel> Languages
		{
			get
			{
				if (languages.Count == 0)
				{

					foreach (var language in character.Languages)
					{
						languages.Add(new ProficiencyViewModel(language));
					}

				}
				return languages;
			}
		}

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

		public ObservableCollection<string> Alignments => _alignments;

		private int selectedAlignment = -1;

		public int SelectedAlignment
		{
			get { return selectedAlignment; }
			set
			{
				if (selectedAlignment < _alignments.Count)
				{
					selectedAlignment = value;
					character.Alignment = _alignments[value];
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

				if (int.TryParse(value, out int result))
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
				int result;
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
				if (int.TryParse(value, out int result))
				{
					character.ArmorClass = result;
				}
			}
		}

		public string Initiative => character.Initiative.ToString();

		public string Speed
		{
			get { return character.Speed.ToString(); }
			set
			{
				if (int.TryParse(value, out int result))
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
				if (int.TryParse(value, out int result))
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
				if (int.TryParse(value, out int result))
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
				if (int.TryParse(value, out int result))
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
				int result;
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
				int result;
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

				int result;
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

				int result;
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

				int result;
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

				int result;
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

				int result;
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

		public string StrengthMod => "+" + character.AbilityModifiers[MainStatType.Strength].ToString();
		public string DexterityMod => "+" + character.AbilityModifiers[MainStatType.Dexterity].ToString();

		public string ConstitutionMod => "+" + character.AbilityModifiers[MainStatType.Constitution].ToString();

		public string IntelligenceMod => "+" + character.AbilityModifiers[MainStatType.Intelligence].ToString();

		public string WisdomMod => "+" + character.AbilityModifiers[MainStatType.Wisdom].ToString();
		public string CharismaMod => "+" + character.AbilityModifiers[MainStatType.Charisma].ToString();

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

		private ObservableCollection<ProficiencyViewModel> weaponProficiencies = new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> WeaponProficiencies
		{
			get
			{
				if (weaponProficiencies.Count == 0)
				{
					foreach (var proficiency in character.WeaponProficiencies)
					{
						weaponProficiencies.Add(new ProficiencyViewModel(proficiency));
					}
				}
				return weaponProficiencies;
			}
		}

		private ObservableCollection<ProficiencyViewModel> armorProficiencies = new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> ArmorProficiencies
		{
			get
			{
				if (armorProficiencies.Count == 0)
				{
					foreach (var proficiency in character.ArmorProficiencies)
					{
						armorProficiencies.Add(new ProficiencyViewModel(proficiency));
					}

				}
				return armorProficiencies;
			}
		}
		private ObservableCollection<ProficiencyViewModel> proficiencies = new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> Proficiencies
		{
			get
			{
				if (proficiencies.Count == 0)
				{
					foreach (var proficiency in character.Proficiencies)
					{
						proficiencies.Add(new ProficiencyViewModel(proficiency));
					}
				}
				return proficiencies;
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
					DieType dieType;
					if (int.TryParse(matches.Groups[1].Value, out int numDice))
					{
						if (Enum.TryParse(matches.Groups[2].Value, out dieType))
						{
							newWepDamage.Dice.Clear();
							newWepDamage.Dice.Add(dieType, numDice);
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

		private int newSpellLevel;
		public string NewSpellLevel
		{
			get { return newSpellLevel.ToString(); }
			set
			{
				if (int.TryParse(value, out int result))
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

		private bool nSpellHasV;

		public bool NSpellHasV
		{
			get { return nSpellHasV; }
			set
			{
				nSpellHasV = value;
				RaisePropertyChanged();
			}
		}

		private bool nSpellHasS;

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

		private ObservableCollection<string> traitStatBonuses = new ObservableCollection<string>();
		public ObservableCollection<string> TraitStatBonuses
		{
			get
			{
				if (traitStatBonuses.Count == 0)
				{
					foreach (var bonus in CharacterManager.StatBonuses)
					{
						traitStatBonuses.Add(bonus.BonusName);
					}
				}
				return traitStatBonuses;
			}
		}

		public int NewStatIncreaseBonus
		{
			get
			{
				return selectedStatIncrease?.Bonus ?? 1;
			}
			set
			{
				if (selectedStatIncrease != null)
				{
					selectedStatIncrease.Bonus = value;
				}
			}
		}

		private StatIncrease selectedStatIncrease = new StatIncrease();
		private int selectedStatBonus = -1;

		public int SelectedStatBonus
		{
			get { return selectedStatBonus; }
			set
			{
				selectedStatBonus = value;
				selectedStatIncrease = StatIncrease.GetNewByName(traitStatBonuses[selectedStatBonus]);
				selectedStatIncrease.Bonus = NewStatIncreaseBonus;
			}
		}



		#endregion

		#endregion


		#region Commands


		public ICommand SaveCharacter => new RelayCommand(SaveCharacterExecute, CanRunCommand);
		public ICommand DeleteItem => new RelayCommand<Item>(DeleteItemExecute);
		public ICommand DeleteWeapon => new RelayCommand<Weapon>(DeleteWeaponExecute);
		public ICommand AddNewItem => new RelayCommand(AddNewItemExecute, CanRunCommand);
		public ICommand AddNewSpell => new RelayCommand(AddNewSpellExecute, CanRunCommand);
		public ICommand AddNewWeapon => new RelayCommand(AddNewWeaponExecute, CanRunCommand);
		public ICommand AddNewCharTrait => new RelayCommand(AddNewCharTraitExecute);
		public ICommand AddNewLanguage => new RelayCommand<string>(AddNewLanguageExecute);
		public ICommand DeleteCharacter => new RelayCommand(DeleteCharacterExec);
		public ICommand AddNewProficiency => new RelayCommand(AddNewProficiencyExec);
		#region CommandFunctions
		async void SaveCharacterExecute()
		{
			await CharacterManager.SaveCurrentCharacter();
		}

		void DeleteCharacterExec()
		{
			CharacterManager.Characters.Remove(CharacterManager.CurrentCharacter);
		}

		bool CanRunCommand()
		{
			return true;
		}

		void DeleteItemExecute(Item param)
		{
			character.Inventory.Remove(param);
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
			Spell newSpell = new Spell()
			{
				Name = newSpellName,
				Description = newSpellDesc,
				Level = newSpellLevel
			};
			if (AddSpellToGlobalList)
			{
				CharacterManager.AllSpells.Add(newSpell);
				await CharacterManager.SaveSpells();
			}
			character.Spells.Add(newSpell);
			Spells.Add(new SpellViewModel(newSpell));
			RaisePropertyChanged("Spells");
			await CharacterManager.SaveCurrentCharacter();
		}

		async void AddNewWeaponExecute()
		{
			var newWep = new Weapon()
			{
				Name = newWepName,
				Damage = newWepDamage,
				Description = newWepDesc
			};
			if (AddWeaponToGlobalList)
			{
				CharacterManager.AllWeapons.Add(newWep);
				await CharacterManager.SaveWeapons();
			}
			character.Weapons.Add(newWep);
			Weapons.Add(new WeaponViewModel(newWep));
			RaisePropertyChanged("Weapons");
			await CharacterManager.SaveCurrentCharacter();
		}

		async void AddNewCharTraitExecute()
		{
			var newTrait = new Trait(newTraitDesc);
			newTraitDesc = "";
			character.Traits.Add(newTrait);
			CharTraits.Add(new TraitViewModel(newTrait));
			RaisePropertyChanged("CharTraits");
			await CharacterManager.SaveCurrentCharacter();
		}

		void AddNewLanguageExecute(string newLang)
		{
			var newProf = new Proficiency(ProficiencyType.Language, newLang);
			Languages.Add(new ProficiencyViewModel(newProf));
			character.Languages.Add(newProf);
			RaisePropertyChanged("Languages");
		}

		void AddNewProficiencyExec()
		{

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

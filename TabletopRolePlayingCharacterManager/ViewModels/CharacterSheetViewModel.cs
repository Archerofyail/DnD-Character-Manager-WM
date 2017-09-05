using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.Devices.Bluetooth.Advertisement;
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
		private Random rand = new Random();
		Character5E character;
		private static ObservableCollection<string> _alignments = new ObservableCollection<string> { "Chaotic Evil", "Neutral Evil", "Lawful Evil", "Chaotic Neutral", "Neutral", "Lawful Neutral", "Chaotic Good", "Neutral Good", "Lawful Good" };
		public CharacterSheetViewModel()
		{
			character = CharacterManager.CurrentCharacter;
			if (character == null)
			{
				character = CharacterManager.GetNewChar();
			}
		}

		#region CombatRollProperties

		private string attackOrDamageTitle = "Attack";
		public string AttackOrDamageTitle
		{
			get => attackOrDamageTitle;
			set
			{
				attackOrDamageTitle = value;
				RaisePropertyChanged();
			}
		}
		private int attackRollBonus;

		private int damageRoll;
		private int critDamage;

		private string spellOrWeaponAttackName;
		public string SpellOrWeaponAttackName
		{
			get => spellOrWeaponAttackName;
			set
			{
				spellOrWeaponAttackName = value;
				RaisePropertyChanged();
			}
		}

		private string spellOrWeapon = "Weapon";

		public string SpellOrWeapon
		{
			get => spellOrWeapon;
			set
			{
				spellOrWeapon = value;
				RaisePropertyChanged();
			}
		}

		public int AttackRoll
		{
			get
			{
				var final = 0;
				final += rand.Next(1, 21);
				AttackRollString = string.Format("1d20[{0}] + Spell Attack Bonus ({1})", final, attackRollBonus);
				if (final == 20)
				{
					AttackRollCrit = true;
				}
				final += attackRollBonus;
				return final;
			}
		}

		public int DamageRoll
		{
			get
			{
				if (AttackRollCrit)
				{
					damageRoll += critDamage;
				}
				return damageRoll;
			}
		}

		private string attackRollList;

		public string AttackRollList
		{
			get => attackRollList;
			set
			{
				attackRollList = value;
				RaisePropertyChanged();
			}
		}

		private bool attackRollCrit;

		public bool AttackRollCrit
		{
			get => attackRollCrit;
			set
			{
				attackRollCrit = value;
				RaisePropertyChanged();
			}
		}

		public string AttackRollString { get; set; }

		public string DamageRollString { get; set; }
		#endregion

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
						languages.Add(new ProficiencyViewModel(language, RemoveProficiencyEx));
					}

				}
				return languages;
			}
		}

		public string Notes
		{
			get => character.Notes;
			set
			{
				character.Notes = value;
				RaisePropertyChanged();
			}
		}

		public string Campaign
		{
			get => character.Campaign;
			set
			{
				character.Campaign = value;
				RaisePropertyChanged();
			}
		}

		public string Name
		{
			get => character.Name;
			set
			{
				character.Name = value;
				RaisePropertyChanged();
			}
		}
		public string Race
		{
			get => character.Race;
			set
			{
				character.Race = value;
				RaisePropertyChanged();
			}
		}
		public string Class
		{
			get => character.Class;
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
			get
			{
				if (selectedAlignment == -1)
				{
					selectedAlignment = _alignments.IndexOf(CharacterManager.CurrentCharacter.Alignment);
				}
				return selectedAlignment;
			}
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
			get => character.Level.ToString();
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
			get => character.Experience.ToString();
			set
			{
				if (int.TryParse(value, out int result))
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
			get => character.ArmorClass.ToString();
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
			get => character.Speed.ToString();
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
			get => character.CurrHP.ToString();
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
			get => character.MaxHP.ToString();
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
			get => character.TempHP.ToString();
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
			get => character.DeathSaveSuccesses;
			set => character.DeathSaveSuccesses = value;
		}
		public bool[] DeathSaveFails
		{
			get => character.DeathSaveFails;
			set => character.DeathSaveFails = value;
		}
		#endregion

		#region PhysicalTraits

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

		public string Eyes
		{
			get => character.Eyes;
			set
			{
				character.Eyes = value;
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

		public string Hair
		{
			get => character.Hair;
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
			get => character.AbilityScores[MainStatType.Strength].ToString();
			set
			{
				if (int.TryParse(value, out int result))
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
			get => character.AbilityScores[MainStatType.Dexterity].ToString();
			set
			{

				if (int.TryParse(value, out int result))
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
			get => character.AbilityScores[MainStatType.Constitution].ToString();
			set
			{

				if (int.TryParse(value, out int result))
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
			get => character.AbilityScores[MainStatType.Intelligence].ToString();
			set
			{

				if (int.TryParse(value, out int result))
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
			get => character.AbilityScores[MainStatType.Wisdom].ToString();
			set
			{

				if (int.TryParse(value, out int result))
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
			get => character.AbilityScores[MainStatType.Charisma].ToString();
			set
			{

				if (int.TryParse(value, out int result))
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
			get => character.AbilityScoreProficiencies[MainStatType.Strength];
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Strength] = value;
				RaisePropertyChanged();
			}
		}
		public bool DexIsProficient
		{
			get => character.AbilityScoreProficiencies[MainStatType.Dexterity];
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Dexterity] = value;
				RaisePropertyChanged();
			}
		}
		public bool ConIsProficient
		{
			get => character.AbilityScoreProficiencies[MainStatType.Constitution];
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Constitution] = value;
				RaisePropertyChanged();
			}
		}
		public bool IntIsProficient
		{
			get => character.AbilityScoreProficiencies[MainStatType.Intelligence];
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Intelligence] = value;
				RaisePropertyChanged();
			}
		}
		public bool WisIsProficient
		{
			get => character.AbilityScoreProficiencies[MainStatType.Wisdom];
			set
			{
				character.AbilityScoreProficiencies[MainStatType.Wisdom] = value;
				RaisePropertyChanged();
			}
		}
		public bool ChaIsProficient
		{
			get => character.AbilityScoreProficiencies[MainStatType.Charisma];
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
			get => character.PersonalityTraits;
			set => character.PersonalityTraits = value;
		}
		public string Ideals
		{
			get => character.Ideals;
			set => character.Ideals = value;
		}
		public string Bonds
		{
			get => character.Bonds;
			set => character.Bonds = value;
		}
		public string Flaws
		{
			get => character.Flaws;
			set => character.Flaws = value;
		}

		public string Backstory
		{
			get => character.Backstory;
			set
			{
				character.Backstory = value;
				RaisePropertyChanged();
			}
		}

		public string God
		{
			get => character.God;
			set
			{
				character.God = value;
				RaisePropertyChanged();
			}
		}

		public string Relationships
		{
			get => character.RelationshipsAndAllies;
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
						spells.Add(new SpellViewModel(item, SetAttackSpell));
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
						weapons.Add(new WeaponViewModel(item, removeWeaponRelay));
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
						weaponProficiencies.Add(new ProficiencyViewModel(proficiency, RemoveProficiencyEx));
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
						armorProficiencies.Add(new ProficiencyViewModel(proficiency, RemoveProficiencyEx));
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
						proficiencies.Add(new ProficiencyViewModel(proficiency, RemoveProficiencyEx));
					}
				}
				return proficiencies;
			}
		}

		private ObservableCollection<SpellSlotsViewModel> spellSlots = new ObservableCollection<SpellSlotsViewModel>();

		public ObservableCollection<SpellSlotsViewModel> SpellSlots
		{
			get
			{
				if (spellSlots.Count == 0)
				{
					int index = 1;
					foreach (var slot in character.SpellSlots)
					{ 

						spellSlots.Add(new SpellSlotsViewModel(slot, index));
						index++;
					}
				}
				return spellSlots;
			}
		}
		#region GlobalListControl

		private bool addSpellToGlobalList;

		public bool AddSpellToGlobalList
		{
			get => addSpellToGlobalList;
			set
			{
				addSpellToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		private bool addItemToGlobalList;

		public bool AddItemToGlobalList
		{
			get => addItemToGlobalList;
			set
			{
				addItemToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		private bool addWeaponToGlobalList;

		public bool AddWeaponToGlobalList
		{
			get => addWeaponToGlobalList;
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
			get => newItemName;
			set
			{
				newItemName = value;
				RaisePropertyChanged();
			}
		}
		private string newItemDesc = "";
		public string NewItemDesc
		{
			get => newItemDesc;
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
			get => newWepName;
			set
			{
				newWepName = value;
				RaisePropertyChanged();
			}
		}
		private Damage newWepDamage = new Damage();

		public string NewWepDamage
		{
			get => newWepDamage.ToString();
			set
			{
				var matches = Regex.Match(value, @"(\d)([dD]\d{1,3})");
				Debug.WriteLine("Matches: " + matches.Value + ". groups count is " + matches.Groups.Count);
				if (matches.Success && matches.Groups.Count >= 3)
				{
					if (int.TryParse(matches.Groups[1].Value, out int numDice))
					{
						if (Enum.TryParse(matches.Groups[2].Value, out DieType dieType))
						{
							newWepDamage = new Damage();
							newWepDamage.Dice.Clear();
							newWepDamage.Dice.Add(dieType, numDice);
						}
					}
				}
			}
		}

		private int newWepAttackBonus = 0;

		public string NewWepAttackBonus
		{
			get => newWepAttackBonus.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					newWepAttackBonus = result;
				}
			}
		}
		private string newWepDesc = "";
		public string NewWepDesc
		{
			get => newWepDesc;
			set
			{
				newWepDesc = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<WeaponRangeType> WeaponRanges => WeaponViewModel.WeaponRanges;
		public ObservableCollection<MainStatType> WeaponMainStats => WeaponViewModel.MainStatTypes;

		public int NewWepSelectedMainStat { get; set; } = -1;
		public int NewWepSelectedType { get; set; } = -1;
		#endregion

		#region Spell

		private string newSpellName;

		public string NewSpellName
		{
			get => newSpellName;
			set
			{
				newSpellName = value;
				RaisePropertyChanged();
			}
		}

		private static ObservableCollection<string> _levels = new ObservableCollection<string> { "Cantrip", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		public ObservableCollection<string> SpellLevels => _levels;
		private int newSpellLevelIndex;

		public int NewSpellLevelIndex
		{
			get => newSpellLevelIndex;
			set
			{
				if (value < _levels.Count)
				{
					if (_levels[value] == "Cantrip")
					{
						newSpellLevelIndex = 0;
						RaisePropertyChanged();
						return;
					}
					newSpellLevelIndex = int.Parse(_levels[value]);
					RaisePropertyChanged();

				}

			}
		}

		public ObservableCollection<SpellSchool> SpellSchools { get; } = new ObservableCollection<SpellSchool>
		{
			SpellSchool.Abjuration, SpellSchool.Conjuration, SpellSchool.Divination,
			SpellSchool.Enchantment, SpellSchool.Evocation, SpellSchool.Illusion,
			SpellSchool.Necromany, SpellSchool.Transmutation
		};

		private int newSpellSchoolIndex = -1;

		public int NewSpellSchoolIndex
		{
			get => newSpellSchoolIndex;
			set
			{
				newSpellSchoolIndex = value;
				RaisePropertyChanged();
			}
		}

		private string newSpellRange = "";

		public string NewSpellRange
		{
			get => newSpellRange;
			set
			{
				newSpellRange = value;
				RaisePropertyChanged();
			}
		}
		private string newSpellTarget = "";
		public string NewSpellTarget
		{
			get => newSpellTarget;
			set
			{
				newSpellTarget = value;
				RaisePropertyChanged();
			}
		}
		private string newSpellDesc;

		public string NewSpellDesc
		{
			get => newSpellDesc;
			set
			{
				newSpellDesc = value;
				RaisePropertyChanged();
			}
		}

		private string newSpellHigherLevels = "";

		public string NewSpellHigherLevels
		{
			get => this.newSpellHigherLevels;

			set
			{
				newSpellHigherLevels = value;
				RaisePropertyChanged();
			}

		}

		private bool nSpellHasV;

		public bool NSpellHasV
		{
			get => nSpellHasV;
			set
			{
				nSpellHasV = value;
				RaisePropertyChanged();
			}
		}

		private bool nSpellHasS;

		public bool NSpellHasS
		{
			get => nSpellHasS;
			set
			{
				nSpellHasS = value;
				RaisePropertyChanged();
			}
		}


		private string nSpellMat = "";

		public string NSpellMat
		{
			get => nSpellMat;
			set
			{
				nSpellMat = value;
				RaisePropertyChanged();
			}
		}

		private bool newSpellIsAttack = false;

		public bool NewSpellIsAttack
		{
			get => newSpellIsAttack;
			set
			{
				newSpellIsAttack = value;
				RaisePropertyChanged();
			}
		}

		private bool newSpellIsSavingThrow;

		public bool NewSpellIsSavingThrow
		{
			get => newSpellIsSavingThrow;
			set
			{
				newSpellIsSavingThrow = value;
				RaisePropertyChanged();
			}
		}

		private Damage newSpellDamage = new Damage();

		public string NewSpellDamage
		{
			get => newSpellDamage.ToString();
			set
			{
				var matches = Regex.Match(value, @"(\d)([dD]\d{1,3})");
				Debug.WriteLine("Matches: " + matches.Value);
				for (var i = 0; i < matches.Groups.Count; i++)
				{
					Debug.WriteLine("match " + i + " is " + matches.Groups[i].Value);
				}
				if (matches.Success && matches.Groups.Count >= 3)
				{
					var dieType = DieType.D4;
					if (int.TryParse(matches.Groups[1].Value, out int numDice))
					{
						if (Enum.TryParse(matches.Groups[2].Value.ToUpper(), out dieType))
						{
							newSpellDamage.Dice.Clear();
							newSpellDamage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}

		private string newSpellDamageType;
		public string NewSpellDamageType
		{
			get => newSpellDamageType;
			set
			{
				newSpellDamageType = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<string> NewSpellRangeTypes { get; } = new ObservableCollection<string> { "None", "Melee", "Ranged" };
		private int newSpellRangeTypeIndex;

		public int NewSpellRangeTypeIndex
		{
			get => newSpellRangeTypeIndex;
			set
			{
				newSpellRangeTypeIndex = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<MainStatType> Attributes { get; } = new ObservableCollection<MainStatType>
		{
			MainStatType.Strength, MainStatType.Dexterity,
			MainStatType.Constitution, MainStatType.Intelligence,
			MainStatType.Wisdom, MainStatType.Charisma
		};

		private int newSpellSavingThrowAttributeIndex = -1;

		public int NewSpellSavingThrowAttributeIndex
		{
			get => newSpellSavingThrowAttributeIndex;
			set
			{
				newSpellSavingThrowAttributeIndex = value;
				RaisePropertyChanged();
			}
		}

		private string newSpellSavingThrowEffect = "";

		public string NewSpellSavingThrowEffect
		{
			get => newSpellSavingThrowEffect;
			set
			{
				newSpellSavingThrowEffect = value;
				RaisePropertyChanged();
			}
		}

		private Damage newSpellHigherLevelDamage = new Damage();

		public string NewSpellHigherLevelDamage
		{
			get => newSpellHigherLevelDamage.ToString();
			set
			{
				var matches = Regex.Match(value, @"(\d)([dD]\d{1,3})");
				Debug.WriteLine("Matches: " + matches.Value);
				for (var i = 0; i < matches.Groups.Count; i++)
				{
					Debug.WriteLine("match " + i + " is " + matches.Groups[i].Value);
				}
				if (matches.Success && matches.Groups.Count >= 3)
				{
					var dieType = DieType.D4;
					if (int.TryParse(matches.Groups[1].Value, out int numDice))
					{
						if (Enum.TryParse(matches.Groups[2].Value.ToUpper(), out dieType))
						{
							newSpellHigherLevelDamage.Dice.Clear();
							newSpellHigherLevelDamage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}

		private bool newSpellAddAbilityModToDamage = false;

		public bool NewSpellAddAbilityModToDamage
		{
			get => newSpellAddAbilityModToDamage;
			set
			{
				newSpellAddAbilityModToDamage = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region Traits

		private string newTraitDesc = "";

		public string NewTraitDesc
		{
			get => newTraitDesc;
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
			get => selectedStatIncrease?.Bonus ?? 1;
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
			get => selectedStatBonus;
			set
			{
				selectedStatBonus = value;
				selectedStatIncrease = StatIncrease.GetNewByName(traitStatBonuses[selectedStatBonus]);
				selectedStatIncrease.Bonus = NewStatIncreaseBonus;
			}
		}



		#endregion

		#region Proficiency

		public ObservableCollection<ProficiencyType> ProficiencyTypes => new ObservableCollection<ProficiencyType>
		{ ProficiencyType.Weapon, ProficiencyType.Armor, ProficiencyType.Language, ProficiencyType.Tool, ProficiencyType.Vehicle };

		private int newProficiencyTypeIndex = -1;
		public int NewProficiencyTypeIndex
		{
			get => newProficiencyTypeIndex;
			set
			{
				newProficiencyTypeIndex = value;
				RaisePropertyChanged();
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
		public ICommand AddNewProficiency => new RelayCommand<string>(AddNewProficiencyExec);
		private RelayCommand<WeaponViewModel> removeWeaponRelay => new RelayCommand<WeaponViewModel>(RemoveWeaponExec);
		#region CommandFunctions

		async void RemoveWeaponExec(WeaponViewModel weapon)
		{
			character.Weapons.Remove(weapon.weapon);
			Weapons.Remove(weapon);
			RaisePropertyChanged("Weapons");
		}
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

		async void AddNewItemExecute()
		{
			var newItem = new Item(newItemName, newItemDesc);

			character.Inventory.Add(newItem);
			if (Items.Count > 0)
			{
				Items.Add(new ItemViewModel(newItem));
			}
			RaisePropertyChanged("Items");
			await CharacterManager.SaveCurrentCharacter();
			if (AddItemToGlobalList)
			{
				CharacterManager.AllItems.Add(newItem);
				await CharacterManager.SaveItems();
			}
		}

		async void AddNewSpellExecute()
		{
			var newSpell = new Spell
			{
				Name = newSpellName,
				Description = newSpellDesc,
				Level = newSpellLevelIndex
			};
			if (AddSpellToGlobalList)
			{
				CharacterManager.AllSpells.Add(newSpell);
				await CharacterManager.SaveSpells();
			}
			character.Spells.Add(newSpell);
			if (Spells.Count >= 0)
			{
				Spells.Add(new SpellViewModel(newSpell, SetAttackSpell));
			}
			RaisePropertyChanged("Spells");
			await CharacterManager.SaveCurrentCharacter();
		}

		async void AddNewWeaponExecute()
		{
			var newWep = new Weapon
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

			if (Weapons.Count > 0)
			{
				Weapons.Add(new WeaponViewModel(newWep, removeWeaponRelay));
			}
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
			Languages.Add(new ProficiencyViewModel(newProf, RemoveProficiencyEx));
			character.Languages.Add(newProf);
			RaisePropertyChanged("Languages");
		}

		void AddNewProficiencyExec(string name)
		{
			var prof = new Proficiency(ProficiencyTypes[newProficiencyTypeIndex], name);
			switch (ProficiencyTypes[newProficiencyTypeIndex])
			{
				case ProficiencyType.Weapon:
				{
					if (WeaponProficiencies.Count > 0)
					{
						WeaponProficiencies.Add(new ProficiencyViewModel(prof, RemoveProficiencyEx));
					}
					character.WeaponProficiencies.Add(prof);
				}
				break;
				case ProficiencyType.Armor:
				{
					if (ArmorProficiencies.Count > 0)
					{
						WeaponProficiencies.Add(new ProficiencyViewModel(prof, RemoveProficiencyEx));
					}
					character.ArmorProficiencies.Add(prof);
				}
				break;
				default:
				{
					if (Proficiencies.Count > 0)
					{
						Proficiencies.Add(new ProficiencyViewModel(prof, RemoveProficiencyEx));
					}
					character.Proficiencies.Add(prof);
				}
				break;

			}
			NewProficiencyTypeIndex = -1;
		}

		void RemoveProficiencyEx(ProficiencyViewModel prof)
		{
			switch (prof.Type)
			{
				case ProficiencyType.Weapon:
				{
					WeaponProficiencies.Remove(prof);
					character.WeaponProficiencies.Remove(character.WeaponProficiencies.First(x => x.Name == prof.Name));
				}
				break;
				case ProficiencyType.Armor:
				{
					ArmorProficiencies.Remove(prof);
					character.ArmorProficiencies.Remove(character.ArmorProficiencies.First(x => x.Name == prof.Name));
				}
				break;
				default:
				{
					Proficiencies.Remove(prof);
					character.Proficiencies.Remove(character.Proficiencies.First(x => x.Name == prof.Name));
				}
				break;
			}
		}
		#endregion
		#endregion

		#region MiscFunctions

		void SetAttackSpell(Spell spell)
		{
			SpellOrWeaponAttackName = spell.Name;
			attackRollBonus = character.AbilityModifiers[character.SpellcastingAttribute] + character.ProficiencyBonus;
			var d1 = spell.Damage.RollDamage();
			var d2 = spell.Damage2.RollDamage();
			damageRoll = d1 + d2;
			DamageRollString = string.Format("{0}[{1}])", spell.Damage.ToString(), d1) + (spell.Damage2.Dice.Count > 0 ? string.Format(" + {0}[{1}]", spell.Damage2.ToString(), d2) : "");

			RaisePropertyChanged("AttackRoll");
			RaisePropertyChanged("DamageRoll");
		}

		void SetAttackWeapon(Weapon weapon)
		{
			SpellOrWeaponAttackName = weapon.Name;
			attackRollBonus = weapon.AttackBonus + (weapon.IsProficient ? character.ProficiencyBonus : 0) + character.AbilityModifiers[weapon.MainStat];
			damageRoll = weapon.Damage.RollDamage() + weapon.Damage2.RollDamage() + character.AbilityModifiers[weapon.MainStat];
			critDamage = weapon.Damage.RollDamage() - weapon.Damage.Bonus;
			RaisePropertyChanged("AttackRoll");
			RaisePropertyChanged("DamageRoll");
		}

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

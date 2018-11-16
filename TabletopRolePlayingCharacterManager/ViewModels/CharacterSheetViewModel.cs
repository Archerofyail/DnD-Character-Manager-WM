using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	//TODO: Save some values for the view to a json file (like if the global list checkboxes are checked)
	//If the CharacterManager's current character is empty when loading this page, assume a new character was created.
	public class CharacterSheetViewModel : ViewModelBase
	{
		private Random _rand = new Random();
		Character5E _character;

		private static ObservableCollection<string> _alignments = new ObservableCollection<string>
		{
			"Chaotic Evil",
			"Neutral Evil",
			"Lawful Evil",
			"Chaotic Neutral",
			"Neutral",
			"Lawful Neutral",
			"Chaotic Good",
			"Neutral Good",
			"Lawful Good"
		};

		public CharacterSheetViewModel()
		{
			_character = CharacterManager.CurrentCharacter;
			if (_character == null)
			{
				_character = CharacterManager.GetNewChar();
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
			}
		}

		#region CombatRollProperties

		private string _attackOrDamageTitle = "Attack";

		public string AttackOrDamageTitle
		{
			get => _attackOrDamageTitle;
			set
			{
				_attackOrDamageTitle = value;
				RaisePropertyChanged();
			}
		}

		private int _attackRollBonus;
		private int _attackRollAttrBonus;
		private MainStatType _attackRollBonusAttr;

		private int _damageRoll;
		private int _critDamage;

		private string _spellOrWeaponAttackName;

		public string SpellOrWeaponAttackName
		{
			get => _spellOrWeaponAttackName;
			set
			{
				_spellOrWeaponAttackName = value;
				RaisePropertyChanged();
			}
		}

		private string _spellOrWeapon = "Weapon";

		public string SpellOrWeapon
		{
			get => _spellOrWeapon;
			set
			{
				_spellOrWeapon = value;
				RaisePropertyChanged();
			}
		}

		public int AttackRoll
		{
			get
			{
				var final = 0;
				final += _rand.Next(1, 21);
				AttackRollString = string.Format("1d20[{0}] + {2} Attack Bonus [{1}]", final, _attackRollBonus, SpellOrWeapon) +
								   (_attackRollAttrBonus > 0
									   ? string.Format(" + {0}[{1}]", _attackRollBonusAttr, _attackRollAttrBonus)
									   : "");
				RaisePropertyChanged("AttackRollString");
				if (final == 20)
				{
					AttackRollCrit = true;
					DamageRollString += string.Format(" + CRIT[{0}]", _critDamage);
				}
				final += _attackRollBonus;
				return final;
			}
		}

		public int DamageRoll
		{
			get
			{
				if (AttackRollCrit)
				{
					_damageRoll += _critDamage;
				}
				return _damageRoll;
			}
		}

		private string _attackRollList;

		public string AttackRollList
		{
			get => _attackRollList;
			set
			{
				_attackRollList = value;
				RaisePropertyChanged();
			}
		}

		private bool _attackRollCrit;

		public bool AttackRollCrit
		{
			get => _attackRollCrit;
			set
			{
				_attackRollCrit = value;
				RaisePropertyChanged();
			}
		}

		public string AttackRollString { get; set; }

		public string DamageRollString { get; set; }
		public int LastHitDieRoll { get; set; }
		public string HealthBeforeRoll { get; set; }
		public int HitDiceRoll
		{
			get
			{
				HealthBeforeRoll = CurrentHealth;
				RaisePropertyChanged("HealthBeforeRoll");

				var total = _rand.Next(1, int.Parse(_character.HitDiceType.ToString().Substring(1))) +
							_character.AbilityModifiers[MainStatType.Constitution];
				_character.CurrHP = Math.Min(total + _character.CurrHP, _character.MaxHP);
				RaisePropertyChanged("CurrentHealth");
				LastHitDieRoll = total;
				return total;
			}
		}

		#endregion

		#region General

		private ObservableCollection<ProficiencyViewModel> _languages = new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> Languages
		{
			get
			{
				if (_languages.Count == 0)
				{

					foreach (var language in _character.Languages)
					{
						_languages.Add(new ProficiencyViewModel(language, RemoveProficiencyEx));
					}

				}
				return _languages;
			}
		}

		public string Notes
		{
			get => _character.Notes;
			set
			{
				_character.Notes = value;
				RaisePropertyChanged();
			}
		}

		public string Campaign
		{
			get => _character.Campaign;
			set
			{
				_character.Campaign = value;
				RaisePropertyChanged();
			}
		}

		public string Name
		{
			get => _character.Name;
			set
			{
				_character.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Race
		{
			get => _character.Race;
			set
			{
				_character.Race = value;
				RaisePropertyChanged();
			}
		}

		public string Class
		{
			get => _character.Class;
			set
			{
				_character.Class = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<string> Alignments => _alignments;

		private int _selectedAlignment = -1;

		public int SelectedAlignment
		{
			get
			{
				if (_selectedAlignment == -1)
				{
					_selectedAlignment = _alignments.IndexOf(CharacterManager.CurrentCharacter.Alignment);
				}
				return _selectedAlignment;
			}
			set
			{
				if (_selectedAlignment < _alignments.Count)
				{
					_selectedAlignment = value;
					_character.Alignment = _alignments[value];
					RaisePropertyChanged();
				}
			}
		}

		public string Level
		{
			get => _character.Level.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.Level = result;
				}
				RaisePropertyChanged("ProficiencyBonus");
				RaisePropertyChanged("HitDiceMax");
				RaisePropertyChanged();
			}
		}

		public string Experience
		{
			get => _character.Experience.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.Experience = result;
				}

				RaisePropertyChanged();
			}
		}

		#endregion

		#region CombatStats

		public string ArmorClass
		{
			get => _character.ArmorClass.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.ArmorClass = result;
				}
			}
		}

		public string ProficiencyBonus => "+" + _character.ProficiencyBonus;

		public string Initiative => _character.Initiative.ToString();

		public string Speed
		{
			get => _character.Speed.ToString();
			set
			{

				if (int.TryParse(value, out int result))
				{
					if (result % 5 == 0)
					{
						_character.Speed = result;
					}
				}
			}
		}

		public string CurrentHealth
		{
			get => _character.CurrHP.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.CurrHP = result;
				}
				RaisePropertyChanged();
				RaisePropertyChanged("CanUseHitDice");
			}
		}

		public string MaxHealth
		{
			get => _character.MaxHP.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.MaxHP = result;
				}
			}
		}

		public string TemporaryHealth
		{
			get => _character.TempHP.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.TempHP = result;
				}
			}
		}

		public bool[] DeathSaveSuccesses
		{
			get => _character.DeathSaveSuccesses;
			set => _character.DeathSaveSuccesses = value;
		}

		public bool[] DeathSaveFails
		{
			get => _character.DeathSaveFails;
			set => _character.DeathSaveFails = value;
		}

		public string ClassResourceName
		{
			get => _character.ClassResourceName;
			set
			{
				_character.ClassResourceName = value;
				RaisePropertyChanged();
			}
		}

		public string ClassResource
		{
			get => _character.ClassResource.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.ClassResource = result;
				}
				RaisePropertyChanged();
			}
		}

		public List<DieType> DieTypes => new List<DieType> { DieType.D4, DieType.D6, DieType.D8, DieType.D10, DieType.D12 };

		public int HitDiceTypeIndex
		{
			get => DieTypes.IndexOf(_character.HitDiceType);
			set
			{
				_character.HitDiceType = DieTypes[value];
				RaisePropertyChanged();
			}
		}

		public int HitDiceMax => _character.HitDiceMax;

		public int HitDice
		{
			get => _character.HitDice;
			set
			{
				_character.HitDice = value;
				RaisePropertyChanged();
				RaisePropertyChanged("CanUseHitDice");
			}
		}

		public bool CanUseHitDice
		{
			get => _character.HitDice > 0 && _character.CurrHP < _character.MaxHP;
		}

		#endregion

		#region SpellStats

		public string SpellAttackBonus =>
			(_character.ProficiencyBonus + _character.AbilityModifiers[_character.SpellcastingAttribute]).ToString();

		public string SpellSaveDc =>
			(8 + _character.ProficiencyBonus + _character.AbilityModifiers[_character.SpellcastingAttribute]).ToString();

		public string SpellcastingAttribute
		{
			get => _character.SpellcastingAttribute.ToString();
			set
			{
				if (Enum.TryParse(value, out MainStatType result))
				{
					_character.SpellcastingAttribute = result;
				}
				RaisePropertyChanged();
				RaisePropertyChanged("SpellAttackBonus");
				RaisePropertyChanged("SpellSaveDC");
			}
		}

		public int SpellcastingAttributeIndex
		{
			get => Attributes.IndexOf(_character.SpellcastingAttribute);
			set
			{
				_character.SpellcastingAttribute = Attributes[value];
				RaisePropertyChanged("SpellAttackBonus");
				RaisePropertyChanged("SpellSaveDC");
				RaisePropertyChanged();
			}
		}

		#endregion

		#region PhysicalTraits

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

		public string Eyes
		{
			get => _character.Eyes;
			set
			{
				_character.Eyes = value;
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

		public string Hair
		{
			get => _character.Hair;
			set
			{
				_character.Hair = value;
				RaisePropertyChanged();
			}
		}


		#endregion

		#region AbilityScores

		public string Strength
		{
			get => _character.AbilityScores[MainStatType.Strength].ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_character.AbilityScores[MainStatType.Strength] = result;
				}
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("StrengthMod");
				RaisePropertyChanged();
			}
		}

		public string Dexterity
		{
			get => _character.AbilityScores[MainStatType.Dexterity].ToString();
			set
			{

				if (int.TryParse(value, out int result))
				{
					_character.AbilityScores[MainStatType.Dexterity] = result;
				}
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("DexterityMod");
				RaisePropertyChanged();
			}
		}

		public string Constitution
		{
			get => _character.AbilityScores[MainStatType.Constitution].ToString();
			set
			{

				if (int.TryParse(value, out int result))
				{
					_character.AbilityScores[MainStatType.Constitution] = result;
				}
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("ConstitutionMod");
				RaisePropertyChanged();
			}
		}

		public string Intelligence
		{
			get => _character.AbilityScores[MainStatType.Intelligence].ToString();
			set
			{

				if (int.TryParse(value, out int result))
				{
					_character.AbilityScores[MainStatType.Intelligence] = result;
				}
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("IntelligenceMod");
				RaisePropertyChanged();
			}
		}

		public string Wisdom
		{
			get => _character.AbilityScores[MainStatType.Wisdom].ToString();
			set
			{

				if (int.TryParse(value, out int result))
				{
					_character.AbilityScores[MainStatType.Wisdom] = result;
				}
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("WisdomMod");
				RaisePropertyChanged();
			}
		}

		public string Charisma
		{
			get => _character.AbilityScores[MainStatType.Charisma].ToString();
			set
			{

				if (int.TryParse(value, out int result))
				{
					_character.AbilityScores[MainStatType.Charisma] = result;
				}
				_character.CalculateAbilityModifiers();
				_character.CalculateSkillBonuses();
				RaisePropertyChanged("Skills");
				RaiseSkillsChanged();
				RaisePropertyChanged("CharismaMod");
				RaisePropertyChanged();
			}
		}

		#region AbilityModifiers

		public string StrengthMod => "+" + _character.AbilityModifiers[MainStatType.Strength].ToString();
		public string DexterityMod => "+" + _character.AbilityModifiers[MainStatType.Dexterity].ToString();

		public string ConstitutionMod => "+" + _character.AbilityModifiers[MainStatType.Constitution].ToString();

		public string IntelligenceMod => "+" + _character.AbilityModifiers[MainStatType.Intelligence].ToString();

		public string WisdomMod => "+" + _character.AbilityModifiers[MainStatType.Wisdom].ToString();
		public string CharismaMod => "+" + _character.AbilityModifiers[MainStatType.Charisma].ToString();

		#endregion

		#region AbilityProficiencies

		public bool StrIsProficient
		{
			get => _character.AbilityScoreProficiencies[MainStatType.Strength];
			set
			{
				_character.AbilityScoreProficiencies[MainStatType.Strength] = value;
				RaisePropertyChanged();
			}
		}

		public bool DexIsProficient
		{
			get => _character.AbilityScoreProficiencies[MainStatType.Dexterity];
			set
			{
				_character.AbilityScoreProficiencies[MainStatType.Dexterity] = value;
				RaisePropertyChanged();
			}
		}

		public bool ConIsProficient
		{
			get => _character.AbilityScoreProficiencies[MainStatType.Constitution];
			set
			{
				_character.AbilityScoreProficiencies[MainStatType.Constitution] = value;
				RaisePropertyChanged();
			}
		}

		public bool IntIsProficient
		{
			get => _character.AbilityScoreProficiencies[MainStatType.Intelligence];
			set
			{
				_character.AbilityScoreProficiencies[MainStatType.Intelligence] = value;
				RaisePropertyChanged();
			}
		}

		public bool WisIsProficient
		{
			get => _character.AbilityScoreProficiencies[MainStatType.Wisdom];
			set
			{
				_character.AbilityScoreProficiencies[MainStatType.Wisdom] = value;
				RaisePropertyChanged();
			}
		}

		public bool ChaIsProficient
		{
			get => _character.AbilityScoreProficiencies[MainStatType.Charisma];
			set
			{
				_character.AbilityScoreProficiencies[MainStatType.Charisma] = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#endregion

		#region Personality

		public string PersonalityTraits
		{
			get => _character.PersonalityTraits;
			set => _character.PersonalityTraits = value;
		}

		public string Ideals
		{
			get => _character.Ideals;
			set => _character.Ideals = value;
		}

		public string Bonds
		{
			get => _character.Bonds;
			set => _character.Bonds = value;
		}

		public string Flaws
		{
			get => _character.Flaws;
			set => _character.Flaws = value;
		}

		public string Backstory
		{
			get => _character.Backstory;
			set
			{
				_character.Backstory = value;
				RaisePropertyChanged();
			}
		}

		public string God
		{
			get => _character.God;
			set
			{
				_character.God = value;
				RaisePropertyChanged();
			}
		}

		public string Relationships
		{
			get => _character.RelationshipsAndAllies;
			set
			{
				_character.RelationshipsAndAllies = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region Lists

		private ObservableCollection<SkillViewModel> _skills = new ObservableCollection<SkillViewModel>();

		public ObservableCollection<SkillViewModel> Skills
		{
			get
			{
				if (_skills.Count == 0)
				{
					foreach (var skill in _character.Skills)
					{
						_skills.Add(new SkillViewModel(skill));
					}
					RaisePropertyChanged();
				}
				return _skills;
			}
		}

		private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();

		public ObservableCollection<ItemViewModel> Items
		{
			get
			{
				if (_items.Count == 0)
				{
					foreach (var item in _character.Inventory)
					{
						_items.Add(new ItemViewModel(item, RemoveItem));
					}
				}
				return _items;

			}
		}

		private ObservableCollection<SpellViewModel> _spells = new ObservableCollection<SpellViewModel>();

		public ObservableCollection<SpellViewModel> Spells
		{
			get
			{
				if (_spells.Count == 0)
				{
					foreach (var item in _character.Spells)
					{
						_spells.Add(new SpellViewModel(item, SetAttackSpell));
					}
				}
				return _spells;

			}
		}

		private ObservableCollection<WeaponViewModel> _weapons = new ObservableCollection<WeaponViewModel>();

		public ObservableCollection<WeaponViewModel> Weapons
		{
			get
			{
				if (_weapons.Count == 0)
				{
					foreach (var item in _character.Weapons)
					{
						_weapons.Add(new WeaponViewModel(item, RemoveWeaponRelay, SetAttackWeapon));
					}
				}
				return _weapons;

			}
		}

		private ObservableCollection<TraitViewModel> _traits = new ObservableCollection<TraitViewModel>();

		public ObservableCollection<TraitViewModel> CharTraits
		{
			get
			{
				if (_traits.Count == 0)
				{
					foreach (var trait in _character.Traits)
					{
						_traits.Add(new TraitViewModel(trait, RemoveTrait));
					}

				}
				return _traits;
			}
		}

		private ObservableCollection<ProficiencyViewModel> _weaponProficiencies =
			new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> WeaponProficiencies
		{
			get
			{
				if (_weaponProficiencies.Count == 0)
				{
					foreach (var proficiency in _character.WeaponProficiencies)
					{
						_weaponProficiencies.Add(new ProficiencyViewModel(proficiency, RemoveProficiencyEx));
					}
				}
				return _weaponProficiencies;
			}
		}

		private ObservableCollection<ProficiencyViewModel> _armorProficiencies =
			new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> ArmorProficiencies
		{
			get
			{
				if (_armorProficiencies.Count == 0)
				{
					foreach (var proficiency in _character.ArmorProficiencies)
					{
						_armorProficiencies.Add(new ProficiencyViewModel(proficiency, RemoveProficiencyEx));
					}

				}
				return _armorProficiencies;
			}
		}

		private ObservableCollection<ProficiencyViewModel> _proficiencies = new ObservableCollection<ProficiencyViewModel>();

		public ObservableCollection<ProficiencyViewModel> Proficiencies
		{
			get
			{
				if (_proficiencies.Count == 0)
				{
					foreach (var proficiency in _character.Proficiencies)
					{
						_proficiencies.Add(new ProficiencyViewModel(proficiency, RemoveProficiencyEx));
					}
				}
				return _proficiencies;
			}
		}

		private ObservableCollection<SpellSlotsViewModel> _spellSlots = new ObservableCollection<SpellSlotsViewModel>();

		public ObservableCollection<SpellSlotsViewModel> SpellSlots
		{
			get
			{
				if (_spellSlots.Count == 0)
				{
					int index = 1;
					foreach (var slot in _character.SpellSlots)
					{

						_spellSlots.Add(new SpellSlotsViewModel(slot, index));
						index++;
					}
				}
				return _spellSlots;
			}
		}

		#region GlobalListControl

		private bool _addSpellToGlobalList;

		public bool AddSpellToGlobalList
		{
			get => _addSpellToGlobalList;
			set
			{
				_addSpellToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		private bool _addItemToGlobalList;

		public bool AddItemToGlobalList
		{
			get => _addItemToGlobalList;
			set
			{
				_addItemToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		private bool _addWeaponToGlobalList;

		public bool AddWeaponToGlobalList
		{
			get => _addWeaponToGlobalList;
			set
			{
				_addWeaponToGlobalList = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#endregion

		#region StuffToAdd

		#region Item

		private string _newItemName = "";

		public string NewItemName
		{
			get => _newItemName;
			set
			{
				_newItemName = value;
				RaisePropertyChanged();
			}
		}

		private string _newItemDesc = "";

		public string NewItemDesc
		{
			get => _newItemDesc;
			set
			{
				_newItemDesc = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region  Weapon

		private string _newWepName = "";

		public string NewWepName
		{
			get => _newWepName;
			set
			{
				_newWepName = value;
				RaisePropertyChanged();
			}
		}

		private Damage _newWepDamage = new Damage();

		public string NewWepDamage
		{
			get => _newWepDamage.ToString();
			set => _newWepDamage.ParseText(value);
		}

		private int _newWepAttackBonus = 0;

		public string NewWepAttackBonus
		{
			get => _newWepAttackBonus.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					_newWepAttackBonus = result;
				}
			}
		}

		private string _newWepDesc = "";

		public string NewWepDesc
		{
			get => _newWepDesc;
			set
			{
				_newWepDesc = value;
				RaisePropertyChanged();
			}
		}

		private bool _newWepIsProficient;

		public bool NewWepIsProficient
		{
			get => _newWepIsProficient;
			set
			{
				_newWepIsProficient = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<WeaponRangeType> WeaponRanges => WeaponViewModel.WeaponRanges;
		public ObservableCollection<MainStatType> WeaponMainStats => WeaponViewModel.MainStatTypes;

		public int NewWepSelectedMainStat { get; set; } = -1;
		public int NewWepSelectedType { get; set; } = -1;

		#endregion

		#region Spell

		private string _newSpellName;

		public string NewSpellName
		{
			get => _newSpellName;
			set
			{
				_newSpellName = value;
				RaisePropertyChanged();
			}
		}

		private static ObservableCollection<string> _levels =
			new ObservableCollection<string> { "Cantrip", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		public ObservableCollection<string> SpellLevels => _levels;
		private int _newSpellLevelIndex;

		public int NewSpellLevelIndex
		{
			get => _newSpellLevelIndex;
			set
			{
				if (value < _levels.Count)
				{
					if (_levels[value] == "Cantrip")
					{
						_newSpellLevelIndex = 0;
						RaisePropertyChanged();
						return;
					}
					_newSpellLevelIndex = int.Parse(_levels[value]);
					RaisePropertyChanged();

				}

			}
		}

		public ObservableCollection<SpellSchool> SpellSchools { get; } = new ObservableCollection<SpellSchool>
		{
			SpellSchool.Abjuration,
			SpellSchool.Conjuration,
			SpellSchool.Divination,
			SpellSchool.Enchantment,
			SpellSchool.Evocation,
			SpellSchool.Illusion,
			SpellSchool.Necromany,
			SpellSchool.Transmutation
		};

		private int _newSpellSchoolIndex = -1;

		public int NewSpellSchoolIndex
		{
			get => _newSpellSchoolIndex;
			set
			{
				_newSpellSchoolIndex = value;
				RaisePropertyChanged();
			}
		}

		private string _newSpellRange = "";

		public string NewSpellRange
		{
			get => _newSpellRange;
			set
			{
				_newSpellRange = value;
				RaisePropertyChanged();
			}
		}

		private string _newSpellTarget = "";

		public string NewSpellTarget
		{
			get => _newSpellTarget;
			set
			{
				_newSpellTarget = value;
				RaisePropertyChanged();
			}
		}

		private string _newSpellDesc;

		public string NewSpellDesc
		{
			get => _newSpellDesc;
			set
			{
				_newSpellDesc = value;
				RaisePropertyChanged();
			}
		}

		private string _newSpellHigherLevels = "";

		public string NewSpellHigherLevels
		{
			get => this._newSpellHigherLevels;

			set
			{
				_newSpellHigherLevels = value;
				RaisePropertyChanged();
			}

		}

		private bool _nSpellHasV;

		public bool NSpellHasV
		{
			get => _nSpellHasV;
			set
			{
				_nSpellHasV = value;
				RaisePropertyChanged();
			}
		}

		private bool _nSpellHasS;

		public bool NSpellHasS
		{
			get => _nSpellHasS;
			set
			{
				_nSpellHasS = value;
				RaisePropertyChanged();
			}
		}


		private string _nSpellMat = "";

		public string NSpellMat
		{
			get => _nSpellMat;
			set
			{
				_nSpellMat = value;
				RaisePropertyChanged();
			}
		}

		private bool _newSpellIsAttack = false;

		public bool NewSpellIsAttack
		{
			get => _newSpellIsAttack;
			set
			{
				_newSpellIsAttack = value;
				RaisePropertyChanged();
			}
		}

		private bool _newSpellIsSavingThrow;

		public bool NewSpellIsSavingThrow
		{
			get => _newSpellIsSavingThrow;
			set
			{
				_newSpellIsSavingThrow = value;
				RaisePropertyChanged();
			}
		}

		private Damage _newSpellDamage = new Damage();

		public string NewSpellDamage
		{
			get => _newSpellDamage.ToString();
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
							_newSpellDamage.Dice.Clear();
							_newSpellDamage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}

		private string _newSpellDamageType;

		public string NewSpellDamageType
		{
			get => _newSpellDamageType;
			set
			{
				_newSpellDamageType = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<string> NewSpellRangeTypes { get; } =
			new ObservableCollection<string> { "None", "Melee", "Ranged" };

		private int _newSpellRangeTypeIndex;

		public int NewSpellRangeTypeIndex
		{
			get => _newSpellRangeTypeIndex;
			set
			{
				_newSpellRangeTypeIndex = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<MainStatType> Attributes { get; } = new ObservableCollection<MainStatType>
		{
			MainStatType.Strength,
			MainStatType.Dexterity,
			MainStatType.Constitution,
			MainStatType.Intelligence,
			MainStatType.Wisdom,
			MainStatType.Charisma
		};

		private int _newSpellSavingThrowAttributeIndex = -1;

		public int NewSpellSavingThrowAttributeIndex
		{
			get => _newSpellSavingThrowAttributeIndex;
			set
			{
				_newSpellSavingThrowAttributeIndex = value;
				RaisePropertyChanged();
			}
		}

		private string _newSpellSavingThrowEffect = "";

		public string NewSpellSavingThrowEffect
		{
			get => _newSpellSavingThrowEffect;
			set
			{
				_newSpellSavingThrowEffect = value;
				RaisePropertyChanged();
			}
		}

		private Damage _newSpellHigherLevelDamage = new Damage();

		public string NewSpellHigherLevelDamage
		{
			get => _newSpellHigherLevelDamage.ToString();
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
							_newSpellHigherLevelDamage.Dice.Clear();
							_newSpellHigherLevelDamage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}

		private bool _newSpellAddAbilityModToDamage = false;

		public bool NewSpellAddAbilityModToDamage
		{
			get => _newSpellAddAbilityModToDamage;
			set
			{
				_newSpellAddAbilityModToDamage = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region Traits

		private string _newTraitName = "";

		public string NewTraitName
		{
			get => _newTraitName;
			set
			{
				_newTraitName = value;
				RaisePropertyChanged();
			}
		}

		private string _newTraitDesc = "";

		public string NewTraitDesc
		{
			get => _newTraitDesc;
			set
			{
				_newTraitDesc = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<TraitSource> TraitSources => new ObservableCollection<TraitSource>
		{
			TraitSource.Background,
			TraitSource.Race,
			TraitSource.Class,
			TraitSource.Other
		};

		private int _newTraitSourceIndex = -1;

		public int NewTraitSourceIndex
		{
			get => _newTraitSourceIndex;
			set
			{
				_newTraitSourceIndex = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<string> _traitStatBonuses = new ObservableCollection<string>();

		public ObservableCollection<string> TraitStatBonuses
		{
			get
			{
				if (_traitStatBonuses.Count == 0)
				{
					foreach (var bonus in CharacterManager.StatBonuses)
					{
						_traitStatBonuses.Add(bonus.BonusName);
					}
				}
				return _traitStatBonuses;
			}
		}

		public int NewStatIncreaseBonus
		{
			get => _selectedStatIncrease?.Bonus ?? 1;
			set
			{
				if (_selectedStatIncrease != null)
				{
					_selectedStatIncrease.Bonus = value;
				}
			}
		}

		private StatIncrease _selectedStatIncrease = new StatIncrease();
		private int _selectedStatBonus = -1;

		public int SelectedStatBonus
		{
			get => _selectedStatBonus;
			set
			{
				_selectedStatBonus = value;
				_selectedStatIncrease = StatIncrease.GetNewByName(_traitStatBonuses[_selectedStatBonus]);
				_selectedStatIncrease.Bonus = NewStatIncreaseBonus;
			}
		}



		#endregion

		#region Proficiency

		public ObservableCollection<ProficiencyType> ProficiencyTypes => new ObservableCollection<ProficiencyType>
		{
			ProficiencyType.Weapon,
			ProficiencyType.Armor,
			ProficiencyType.Language,
			ProficiencyType.Tool,
			ProficiencyType.Vehicle
		};

		private int _newProficiencyTypeIndex = -1;

		public int NewProficiencyTypeIndex
		{
			get => _newProficiencyTypeIndex;
			set
			{
				_newProficiencyTypeIndex = value;
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
		public ICommand RollDamage => new RelayCommand(RollDamageEx);

		public ICommand HitDieUsed => new RelayCommand(HitDieUsedEx);
		private RelayCommand<WeaponViewModel> RemoveWeaponRelay => new RelayCommand<WeaponViewModel>(RemoveWeaponExec);

		#region CommandFunctions

		void HitDieUsedEx()
		{
			HitDice--;
			RaisePropertyChanged("HitDiceRoll");
		}

		void RollDamageEx()
		{
			AttackOrDamageTitle = "Damage";
			
		}

		void RemoveWeaponExec(WeaponViewModel weapon)
		{
			_character.Weapons.Remove(weapon.Weapon);
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
			_character.Inventory.Remove(param);
		}

		void DeleteWeaponExecute(Weapon param)
		{
			_character.Weapons.Remove(param);
		}

		async void AddNewItemExecute()
		{
			var newItem = new Item(_newItemName, _newItemDesc);


			if (Items.Count > 0)
			{
				Items.Add(new ItemViewModel(newItem, RemoveItem));
			}
			_character.Inventory.Add(newItem);
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
				Name = _newSpellName,
				Description = _newSpellDesc,
				Level = _newSpellLevelIndex
			};
			if (Spells.Count > 0)
			{
				Spells.Add(new SpellViewModel(newSpell, SetAttackSpell));
			}
			if (AddSpellToGlobalList)
			{
				CharacterManager.AllSpells.Add(newSpell);
				await CharacterManager.SaveSpells();
			}
			_character.Spells.Add(newSpell);

			RaisePropertyChanged("Spells");
			await CharacterManager.SaveCurrentCharacter();
		}

		async void AddNewWeaponExecute()
		{
			var newWep = new Weapon
			{
				Name = _newWepName,
				Damage = _newWepDamage,
				Description = _newWepDesc,
				MainStat = WeaponMainStats[NewWepSelectedMainStat],
				AttackBonus = _newWepAttackBonus,
				WeaponRangeType = WeaponRanges[NewWepSelectedType],
				IsProficient = NewWepIsProficient
			};
			if (Weapons.Count > 0)
			{
				Weapons.Add(new WeaponViewModel(newWep, RemoveWeaponRelay, SetAttackWeapon));
			}
			if (AddWeaponToGlobalList)
			{
				CharacterManager.AllWeapons.Add(newWep);
				await CharacterManager.SaveWeapons();
			}



			_character.Weapons.Add(newWep);

			RaisePropertyChanged("Weapons");
			await CharacterManager.SaveCurrentCharacter();
		}

		async void AddNewCharTraitExecute()
		{
			var newTrait = new Trait(_newTraitName, _newTraitDesc, TraitSources[_newTraitSourceIndex]);
			NewTraitDesc = "";
			NewTraitName = "";
			NewTraitSourceIndex = -1;
			if (CharTraits.Count > 0)
			{
				CharTraits.Add(new TraitViewModel(newTrait, RemoveTrait));
			}
			_character.Traits.Add(newTrait);
			RaisePropertyChanged("CharTraits");
			await CharacterManager.SaveCurrentCharacter();
		}

		void AddNewLanguageExecute(string newLang)
		{
			var newProf = new Proficiency(ProficiencyType.Language, newLang);
			if (Languages.Count > 0)
			{
				Languages.Add(new ProficiencyViewModel(newProf, RemoveProficiencyEx));
			}
			_character.Languages.Add(newProf);
			_character.Proficiencies.Add(newProf);
			RaisePropertyChanged("Languages");
		}

		void AddNewProficiencyExec(string name)
		{
			if (_character.Proficiencies.Exists(x => x.Name == name))
			{
				return;
			}

			var prof = new Proficiency(ProficiencyTypes[_newProficiencyTypeIndex], name);
			if (Proficiencies.Count > 0)
			{
				Proficiencies.Add(new ProficiencyViewModel(prof, RemoveProficiencyEx));
			}
			_character.Proficiencies.Add(prof);
			RaisePropertyChanged("Proficiencies");
			NewProficiencyTypeIndex = -1;
		}

		void RemoveProficiencyEx(ProficiencyViewModel prof)
		{


			Proficiencies.Remove(prof);
			
			if (prof.Type == ProficiencyType.Language)
			{
				_character.Languages.Remove(_character.Languages.First(x => x.Name == prof.Name));
				Languages.Remove(prof);
			}
			_character.Proficiencies.Remove(_character.Proficiencies.First(x => x.Name == prof.Name));
			RaisePropertyChanged("Proficiencies");
			RaisePropertyChanged("Languages");

		}
		#endregion
		#endregion

		#region MiscFunctions

		void SetAttackSpell(Spell spell, int atLevel = 0)
		{
			SpellOrWeaponAttackName = spell.Name;
			_attackRollBonus = _character.AbilityModifiers[_character.SpellcastingAttribute] + _character.ProficiencyBonus;
			var d1 = spell.Damage.RollDamage();
			var d2 = spell.Damage2.RollDamage();
			SpellOrWeapon = "Spell Modifier";
			_damageRoll = d1 + d2 + (spell.AddAbilityModToDamage ? _character.AbilityModifiers[_character.SpellcastingAttribute] : 0);
			DamageRollString = string.Format("{0}[{1}]", spell.Damage.ToString(), d1) + (spell.Damage2.Dice.Count > 0 ? string.Format(" + {0}[{1}]", spell.Damage2.ToString(), d2) : "") +
				(spell.AddAbilityModToDamage ? string.Format(" + {0}[{1}]", _character.SpellcastingAttribute, _character.AbilityModifiers[_character.SpellcastingAttribute]) : "");
			RaisePropertyChanged("AttackRoll");
			RaisePropertyChanged("DamageRoll");
		}

		void SetAttackWeapon(Weapon weapon)
		{
			SpellOrWeaponAttackName = weapon.Name;
			_attackRollBonus = weapon.AttackBonus + (weapon.IsProficient ? _character.ProficiencyBonus : 0) + _character.AbilityModifiers[weapon.MainStat];
			var d1 = weapon.Damage.RollDamage();
			var d2 = 0;
			_damageRoll = d1 + _character.AbilityModifiers[weapon.MainStat];
			if (weapon.Damage2 != null)
			{
				_damageRoll += weapon.Damage2.RollDamage();
			}
			_attackRollBonusAttr = weapon.MainStat;
			_attackRollAttrBonus = _character.AbilityModifiers[weapon.MainStat];
			_critDamage = weapon.Damage.RollDamage() - weapon.Damage.Bonus;
			DamageRollString = string.Format("{0}[{1}]", weapon.Damage.ToString(), d1) + (d2 > 0 ? string.Format(" + {0}[{1}]", weapon.Damage2.ToString(), d2) : "") +
								string.Format(" + {0}[{1}]", MainStatType.Strength, _character.AbilityModifiers[MainStatType.Strength]);

			RaisePropertyChanged("AttackRoll");
			RaisePropertyChanged("DamageRoll");
		}

		void RemoveTrait(TraitViewModel trait)
		{
			CharTraits.Remove(trait);
			_character.Traits.Remove(trait.Trait);
		}

		void RemoveItem(ItemViewModel item)
		{
			Items.Remove(item);
			_character.Inventory.Remove(item.Item);
		}
		async void RaiseSkillsChanged()
		{
			foreach (var skillViewModel in Skills)
			{
				skillViewModel.RaiseAllPropertiesChanged();
			}
			await CharacterManager.SaveCurrentCharacter();
		}
		#endregion

		public override async void RaisePropertyChanged([CallerMemberName]string propertyName = null)
		{
			await CharacterManager.SaveCurrentCharacter();
			base.RaisePropertyChanged(propertyName);
		}
	}
}

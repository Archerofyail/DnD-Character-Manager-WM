using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class SpellViewModel : ViewModelBase
	{
		private Spell _spell;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="spell"></param>
		/// <param name="atLevel">When at 0 defaults to the level of the spell. Not applicable to cantrips (level 0)</param>
		public delegate void RollSpellAttackDelegate(Spell spell, int atLevel = 0);

		public RollSpellAttackDelegate RollSpellAttack;
		private static ObservableCollection<string> _levels = new ObservableCollection<string> { "Cantrip", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		public SpellViewModel(Spell sp, RollSpellAttackDelegate attackMethod = null)
		{
			_spell = sp;
			RollSpellAttack = attackMethod;
		}

		public string Name
		{
			get => _spell.Name;
			set
			{
				_spell.Name = value;
				RaisePropertyChanged();
			}
		}

		public int LevelIndex
		{
			get => _spell.Level;
			set
			{
				if (value < _levels.Count && value >= 0)
				{

					_spell.Level = value;
					RaisePropertyChanged();
					RaisePropertyChanged("IsNotCantrip");


				}
			}
		}
		public ObservableCollection<string> SpellLevels => _levels;

		public string Description
		{
			get => _spell.Description;
			set
			{
				_spell.Description = value;
				RaisePropertyChanged();
			}
		}

		public bool HasVerbalComponent
		{
			get => _spell.HasVerbalComponent;
			set
			{
				_spell.HasVerbalComponent = value;
				RaisePropertyChanged();
			}
		}

		public bool HasSomaticComponent
		{
			get => _spell.HasSomaticComponent;
			set
			{
				_spell.HasSomaticComponent = value;
				RaisePropertyChanged();
			}
		}

		public string MaterialComponent
		{
			get => _spell.MaterialComponent;
			set => _spell.MaterialComponent = value;
		}

		public static ObservableCollection<SpellSchool> SpellSchools { get; } = new ObservableCollection<SpellSchool>
		{
			SpellSchool.Abjuration, SpellSchool.Conjuration, SpellSchool.Divination,
			SpellSchool.Enchantment, SpellSchool.Evocation, SpellSchool.Illusion,
			SpellSchool.Necromany, SpellSchool.Transmutation
		};


		public int SchoolIndex
		{
			get => SpellSchools.IndexOf(_spell.School);
			set
			{
				if (value >= 0 && value < SpellSchools.Count)
				{
					_spell.School = SpellSchools[value];
				}
				RaisePropertyChanged();
			}
		}


		public string Range
		{
			get => _spell.Range;
			set
			{
				_spell.Range = value;
				RaisePropertyChanged();
			}
		}

		public string Target
		{
			get => _spell.Target;
			set
			{
				_spell.Target = value;
				RaisePropertyChanged();
			}
		}


		public string HigherLevels
		{
			get => _spell.AtHigherLevelsDescription;

			set
			{
				_spell.AtHigherLevelsDescription = value;
				RaisePropertyChanged();
			}

		}


		public bool IsAttack
		{
			get => _spell.IsAttack;
			set
			{
				_spell.IsAttack = value;
				RaisePropertyChanged();
			}
		}

		public bool IsNotCantrip => _spell.Level != 0;

		public bool IsSavingThrow
		{
			get => !_spell.IsAttackRoll;
			set
			{
				_spell.IsAttackRoll = !value;
				RaisePropertyChanged();
			}
		}

		public string Damage
		{
			get => _spell.Damage.ToString();
			set
			{
				_spell.Damage.ParseText(value);
				RaisePropertyChanged();
			}
		}


		public string DamageType
		{
			get => _spell.DamageType;
			set
			{
				_spell.DamageType = value;
				RaisePropertyChanged();
			}
		}

		public static ObservableCollection<string> SpellRangeTypes { get; } = new ObservableCollection<string> { "None", "Melee", "Ranged" };

		public int RangeTypeIndex
		{
			get => SpellRangeTypes.IndexOf(_spell.RangeType);
			set
			{
				if (value >= 0 && value < SpellRangeTypes.Count)
				{
					_spell.RangeType = SpellRangeTypes[value];
				}

				RaisePropertyChanged();
			}
		}

		public ObservableCollection<MainStatType> Attributes { get; } = new ObservableCollection<MainStatType>
		{
			MainStatType.Strength, MainStatType.Dexterity,
			MainStatType.Constitution, MainStatType.Intelligence,
			MainStatType.Wisdom, MainStatType.Charisma
		};


		public int SavingThrowAttributeIndex
		{
			get => Attributes.IndexOf(_spell.SavingThrowAttribute);
			set
			{
				if (value >= 0 && value < Attributes.Count)
				{
					_spell.SavingThrowAttribute = Attributes[value];
				}
				RaisePropertyChanged();
			}
		}


		public string SavingThrowEffect
		{
			get => _spell.SavingThrowEffect;
			set
			{
				_spell.SavingThrowEffect = value;
				RaisePropertyChanged();
			}
		}


		public string HigherLevelDamage
		{
			get => _spell.HigherLevelDamage.ToString();
			set
			{
				_spell.HigherLevelDamage.ParseText(value);
				RaisePropertyChanged();
			}
		}

		public bool AddAbilityModToDamage
		{
			get => _spell.AddAbilityModToDamage;
			set
			{
				_spell.AddAbilityModToDamage = value;
				RaisePropertyChanged();
			}
		}

		#region ReadOnlyProperties

		public string LevelAndSchool => LevelIndex == 0
			? SpellSchools[SchoolIndex] + " cantrip"
			: "Level " + LevelIndex + " " + SpellSchools[SchoolIndex];

		public string AttackButtonText => IsSavingThrow ? "Roll Damage" : "Roll Attack";
		#endregion

		#region ViewEvents


		public bool HasDescription => Description?.Length > 0;

		private bool _isEditing;
		public bool IsEditing
		{
			get => _isEditing;
			set
			{
				_isEditing = value;
				RaisePropertyChanged();
				RaisePropertyChanged("IsNotEditing");
			}
		}

		public bool IsNotEditing => !IsEditing;

		public ICommand StartEditing => new RelayCommand(StartEditingEx);
		public ICommand StopEditing => new RelayCommand(StopEditingEx);
		void StartEditingEx()
		{
			IsEditing = true;
		}

		void StopEditingEx()
		{
			IsEditing = false;
			RaisePropertyChanged("LevelAndSchool");
			RaisePropertyChanged("AttackButtonText");
			RaisePropertyChanged("Description");
			RaisePropertyChanged("HigherLevels");
		}


		public ICommand RollAttack => new RelayCommand(RollAttackEx);

		void RollAttackEx()
		{
			RollSpellAttack?.Invoke(_spell);
		}

		#endregion


	}
}

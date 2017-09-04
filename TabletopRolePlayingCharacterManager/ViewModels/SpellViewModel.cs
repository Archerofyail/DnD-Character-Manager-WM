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
		private Spell spell;
		private static ObservableCollection<string> _levels = new ObservableCollection<string> { "Cantrip", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		public SpellViewModel(Spell sp)
		{
			spell = sp;
		}

		public string Name
		{
			get => spell.Name;
			set
			{
				spell.Name = value;
				RaisePropertyChanged();
			}
		}

		public int LevelIndex
		{
			get { return spell.Level; }
			set
			{
				if (value < _levels.Count && value >= 0)
				{

					spell.Level = value;
					RaisePropertyChanged();

				}
			}
		}
		public ObservableCollection<string> SpellLevels => _levels;

		public string Description
		{
			get => spell.Description;
			set
			{
				spell.Description = value;
				RaisePropertyChanged();
			}
		}

		public bool HasVerbalComponent
		{
			get => spell.HasVerbalComponent;
			set
			{
				spell.HasVerbalComponent = value;
				RaisePropertyChanged();
			}
		}

		public bool HasSomaticComponent
		{
			get => spell.HasSomaticComponent;
			set
			{
				spell.HasSomaticComponent = value;
				RaisePropertyChanged();
			}
		}

		public string MaterialComponent
		{
			get => spell.MaterialComponent;
			set => spell.MaterialComponent = value;
		}

		public static ObservableCollection<SpellSchool> SpellSchools { get; } = new ObservableCollection<SpellSchool>
		{
			SpellSchool.Abjuration, SpellSchool.Conjuration, SpellSchool.Divination,
			SpellSchool.Enchantment, SpellSchool.Evocation, SpellSchool.Illusion,
			SpellSchool.Necromany, SpellSchool.Transmutation
		};


		public int SchoolIndex
		{
			get => SpellSchools.IndexOf(spell.School);
			set
			{
				if (value >= 0 && value < SpellSchools.Count)
				{
					spell.School = SpellSchools[value];
				}
				RaisePropertyChanged();
			}
		}


		public string Range
		{
			get => spell.Range;
			set
			{
				spell.Range = value;
				RaisePropertyChanged();
			}
		}

		public string Target
		{
			get => spell.Target;
			set
			{
				spell.Target = value;
				RaisePropertyChanged();
			}
		}


		public string HigherLevels
		{
			get => spell.AtHigherLevelsDescription;

			set
			{
				spell.AtHigherLevelsDescription = value;
				RaisePropertyChanged();
			}

		}


		public bool IsAttack
		{
			get => spell.IsAttack;
			set
			{
				spell.IsAttack = value;
				RaisePropertyChanged();
			}
		}

		public bool IsSavingThrow
		{
			get => !spell.IsAttackRoll;
			set
			{
				spell.IsAttackRoll = !value;
				RaisePropertyChanged();
			}
		}

		public string Damage
		{
			get => spell.Damage.ToString();
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
							spell.Damage.Dice.Clear();
							spell.Damage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}


		public string DamageType
		{
			get => spell.DamageType;
			set
			{
				spell.DamageType = value;
				RaisePropertyChanged();
			}
		}

		public static ObservableCollection<string> SpellRangeTypes { get; } = new ObservableCollection<string> { "None", "Melee", "Ranged" };

		public int RangeTypeIndex
		{
			get => SpellRangeTypes.IndexOf(spell.RangeType);
			set
			{
				if (value >= 0 && value < SpellRangeTypes.Count)
				{
					spell.RangeType = SpellRangeTypes[value];
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
			get => Attributes.IndexOf(spell.SavingThrowAttribute);
			set
			{
				if (value >= 0 && value < Attributes.Count)
				{
					spell.SavingThrowAttribute = Attributes[value];
				}
				RaisePropertyChanged();
			}
		}


		public string SavingThrowEffect
		{
			get => spell.SavingThrowEffect;
			set
			{
				spell.SavingThrowEffect = value;
				RaisePropertyChanged();
			}
		}


		public string HigherLevelDamage
		{
			get => spell.HigherLevelDamage.ToString();
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
							spell.HigherLevelDamage.Dice.Clear();
							spell.HigherLevelDamage.Dice.Add(dieType, numDice);
						}
					}
				}
				RaisePropertyChanged();
			}
		}

		public bool AddAbilityModToDamage
		{
			get => spell.AddAbilityModToDamage;
			set
			{
				spell.AddAbilityModToDamage = value;
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



		private bool isEditing;
		public bool IsEditing
		{
			get => isEditing;
			set
			{
				isEditing = value;
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

		}

		#endregion


	}
}

using System;
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
	public class WeaponViewModel : ViewModelBase
	{
		public Weapon Weapon { get; }
		public static ObservableCollection<MainStatType> MainStatTypes = new ObservableCollection<MainStatType>() { MainStatType.Strength, MainStatType.Dexterity };
		public static  ObservableCollection<WeaponRangeType> WeaponRanges = new ObservableCollection<WeaponRangeType>() {WeaponRangeType.Melee, WeaponRangeType.Ranged};
		private RelayCommand<WeaponViewModel> _remove;
		public delegate void SetAttackWeaponDelegate(Weapon weapon);

		private SetAttackWeaponDelegate _setAttackWeapon;
		public WeaponViewModel(Weapon weap, RelayCommand<WeaponViewModel> removeCommand, SetAttackWeaponDelegate setAttackWeap)
		{
			Weapon = weap;
			_selectedMainStat = MainStatTypes.IndexOf(weap.MainStat);
			SelectedWeaponType = WeaponRanges.IndexOf(weap.WeaponRangeType);
			_remove = removeCommand;
			_setAttackWeapon = setAttackWeap;
		}
		public string Name
		{
			get => Weapon.Name;
			set
			{
				Weapon.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Damage
		{
			get => Weapon.Damage.ToString();
			set
			{
				Weapon.Damage.ParseText(value);
				RaisePropertyChanged();
				RaisePropertyChanged("DamageWithBonus");
			}
		}

		public string DamageWithBonus => Weapon.Damage + " + " + Utility.ShorthandStatStrings[MainStat[SelectedMainStat]] + "[" + CharacterManager.CurrentCharacter.AbilityModifiers[MainStat[SelectedMainStat]] +"]";

		public ObservableCollection<MainStatType> MainStat => MainStatTypes;

		private int _selectedMainStat = -1;

		public int SelectedMainStat
		{
			get => _selectedMainStat;
			set
			{
				if (value < MainStatTypes.Count && MainStatTypes.Count >= 0)
				{
					_selectedMainStat = value;
					Weapon.MainStat = MainStatTypes[_selectedMainStat];
					RaisePropertyChanged();
				}
			}
		}

		public string AttackBonus
		{
			get => Weapon.AttackBonus.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					Weapon.AttackBonus = result;
					RaisePropertyChanged();
				}
			}
		}

		public int SelectedWeaponType
		{
			get => _selectedWeaponType;
			set
			{
				_selectedWeaponType = value;
				RaisePropertyChanged();
				RaisePropertyChanged("IsRanged");
			}
		}

		public ObservableCollection<WeaponRangeType> WeaponTypes => WeaponRanges;

		public string Range
		{
			get => Weapon.Range;

			set
			{
				Weapon.Range = value;
				RaisePropertyChanged();
			}

		}

		public string RangeString
		{
			get => WeaponRanges[SelectedWeaponType] + (!string.IsNullOrEmpty(Range) ? " - " + Range : "");
		}

		public string Description
		{
			get => Weapon.Description;
			set
			{
				Weapon.Description = value; 
				RaisePropertyChanged();
				RaisePropertyChanged("HasDescription");
			}
		}

		public bool IsProficient
		{
			get => Weapon.IsProficient;
			set
			{
				Weapon.IsProficient = value;
				RaisePropertyChanged();
			}
		}

		public bool IsRanged
		{
			get => Weapon.WeaponRangeType == WeaponRangeType.Ranged;
		}

		public bool HasAttackBonus => Weapon.AttackBonus > 0;

		public ICommand RemoveWeapon => _remove;

		public ICommand Attack => new RelayCommand(RollAttackEx);

		void RollAttackEx()
		{
			_setAttackWeapon?.Invoke(Weapon);
		}

		public bool HasDescription => Description?.Length > 0;


		private bool _isEditing;
		private int _selectedWeaponType = -1;

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
			RaisePropertyChanged("HasDescription");
			RaisePropertyChanged("HigherLevels");
			RaisePropertyChanged("RangeString");
			RaisePropertyChanged("HasAttackBonus");
		}
	}
}

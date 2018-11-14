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
		public Weapon weapon { get; }
		public static ObservableCollection<MainStatType> MainStatTypes = new ObservableCollection<MainStatType>() { MainStatType.Strength, MainStatType.Dexterity };
		public static  ObservableCollection<WeaponRangeType> WeaponRanges = new ObservableCollection<WeaponRangeType>() {WeaponRangeType.Melee, WeaponRangeType.Ranged};
		private RelayCommand<WeaponViewModel> remove;
		public delegate void SetAttackWeaponDelegate(Weapon weapon);

		private SetAttackWeaponDelegate SetAttackWeapon;
		public WeaponViewModel(Weapon weap, RelayCommand<WeaponViewModel> removeCommand, SetAttackWeaponDelegate setAttackWeap)
		{
			weapon = weap;
			selectedMainStat = MainStatTypes.IndexOf(weap.MainStat);
			SelectedWeaponType = WeaponRanges.IndexOf(weap.WeaponRangeType);
			remove = removeCommand;
			SetAttackWeapon = setAttackWeap;
		}
		public string Name
		{
			get => weapon.Name;
			set
			{
				weapon.Name = value;
				RaisePropertyChanged();
			}
		}

		public string Damage
		{
			get => weapon.Damage.ToString();
			set
			{
				weapon.Damage.ParseText(value);
				RaisePropertyChanged();
				RaisePropertyChanged("DamageWithBonus");
			}
		}

		public string DamageWithBonus => weapon.Damage + " + " + Utility.ShorthandStatStrings[MainStat[SelectedMainStat]] + "[" + CharacterManager.CurrentCharacter.AbilityModifiers[MainStat[SelectedMainStat]] +"]";

		public ObservableCollection<MainStatType> MainStat => MainStatTypes;

		private int selectedMainStat = -1;

		public int SelectedMainStat
		{
			get => selectedMainStat;
			set
			{
				if (value < MainStatTypes.Count && MainStatTypes.Count >= 0)
				{
					selectedMainStat = value;
					weapon.MainStat = MainStatTypes[selectedMainStat];
					RaisePropertyChanged();
				}
			}
		}

		public string AttackBonus
		{
			get => weapon.AttackBonus.ToString();
			set
			{
				if (int.TryParse(value, out int result))
				{
					weapon.AttackBonus = result;
					RaisePropertyChanged();
				}
			}
		}

		public int SelectedWeaponType { get; set; } = -1;

		public ObservableCollection<WeaponRangeType> WeaponTypes => WeaponRanges;

		public string Range
		{
			get => weapon.Range;

			set
			{
				weapon.Range = value;
				RaisePropertyChanged();
			}

		}

		public string RangeString
		{
			get => WeaponRanges[SelectedWeaponType] + (!string.IsNullOrEmpty(Range) ? " - " + Range : "");
		}

		public string Description
		{
			get => weapon.Description;
			set
			{
				weapon.Description = value; 
				RaisePropertyChanged();
				RaisePropertyChanged("HasDescription");
			}
		}

		public bool IsProficient
		{
			get => weapon.IsProficient;
			set
			{
				weapon.IsProficient = value;
				RaisePropertyChanged();
			}
		}

		public bool HasAttackBonus => weapon.AttackBonus > 0;

		public ICommand RemoveWeapon => remove;

		public ICommand Attack => new RelayCommand(RollAttackEx);

		void RollAttackEx()
		{
			SetAttackWeapon?.Invoke(weapon);
		}

		public bool HasDescription => Description?.Length > 0;


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
			RaisePropertyChanged("HasDescription");
			RaisePropertyChanged("HigherLevels");
			RaisePropertyChanged("RangeString");
			RaisePropertyChanged("HasAttackBonus");
		}
	}
}

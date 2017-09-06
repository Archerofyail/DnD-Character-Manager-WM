using System;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using TabletopRolePlayingCharacterManager.ViewModels;

namespace TabletopRolePlayingCharacterManager.Views
{
	public sealed partial class CharacterSheet
	{
		public CharacterSheet()
		{
			InitializeComponent();
			SpellList.Loaded += (s, ev) =>
			{
				var attackButtonList = SpellList.FindDescendants<Button>().Where(x => x.Tag != null && x.Tag.Equals("RollAttackButton"));
					
				foreach (var button in attackButtonList)
				{
					button.Click += ShowAttackDialogAsync;
				}
			};
			WeaponList.Loaded += (sender, args) =>
			{
				var atkBtnList = WeaponList.FindDescendants<Button>().Where(x => x.Tag != null && x.Tag.ToString() == "RollAttackButton");
				foreach (var button in atkBtnList)
				{
					button.Click += ShowAttackDialogAsync;
				}

			};
		}

		public async void ShowAttackDialogAsync(object sender, RoutedEventArgs e)
		{
			if ((sender as Button).Content.Equals("Roll Damage"))
			{
				AttackRollDialogPanel.Visibility = Visibility.Collapsed;
				DamageRollDialogPanel.Visibility = Visibility.Visible;
				AttackDialog.IsPrimaryButtonEnabled = false;
			}
			
			await AttackDialog.ShowAsync();
		}

		public void GeneralTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
		{
			ClassAndRaceInfo.Visibility = ClassAndRaceInfo.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			GeneralExpandIcon.Text = ClassAndRaceInfo.Visibility == Visibility.Visible ? "-" : "+";
		}

		private void AbilityScoresHeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			AbilityScores.Visibility = AbilityScores.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			AbilityScoresExpandIcon.Text = AbilityScores.Visibility == Visibility.Visible ? "-" : "+";
		}

		private void PhysicalDescriptionHeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			PhysicalDescription.Visibility = PhysicalDescription.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			PhysicalDescExpandIcon.Text = PhysicalDescription.Visibility == Visibility.Visible ? "-" : "+";
		}

		private void DeleteCharacterClick(object sender, RoutedEventArgs e)
		{

		}

		private void AddSpellButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			AddSpellButton.Flyout?.Hide();
		}

		private void AddItemButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			AddItemButton.Flyout?.Hide();
		}

		private void AddWeaponButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			AddWeaponButton.Flyout?.Hide();
		}

		private void AddTraitTapped(object sender, TappedRoutedEventArgs e)
		{
			AddTraitButton.Flyout?.Hide();
		}

		private void AddNewLangTapped(object sender, TappedRoutedEventArgs e)
		{
			AddNewLangButton.Flyout?.Hide();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			SaveButton.Command?.Execute(SaveButton);
			base.OnNavigatedFrom(e);
		}


		private void DeleteCharacterTapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}

		private void AddNewProficiencyTapped(object sender, TappedRoutedEventArgs e)
		{
			AddNewProficiencyButton.Flyout?.Hide();
		}

		private void TraitListOnItemClick(object sender, ItemClickEventArgs e)
		{

			foreach (var trait in TraitList.Items)
			{
				if (trait != e.ClickedItem)
				{
					(trait as TraitViewModel).StopEditing.Execute(null);

				}
				else
				{
					(trait as TraitViewModel).StartEditing.Execute(null);
					var traitControls = TraitList.ContainerFromItem(trait);
					(traitControls.FindDescendantByName("DescTextBox") as TextBox)?.Focus(FocusState.Programmatic);

				}
			}
		}

		private void IsAttackSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			var chkbox = sender as ToggleSwitch;
			NewSpellAttackStatsPanel.Visibility = (bool)chkbox.IsOn ? Visibility.Visible : Visibility.Collapsed;
		}

		private void DCSaveToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
		{
			var swtch = sender as ToggleSwitch;

			AttackRollPanel.Visibility = (bool)swtch.IsOn ? Visibility.Collapsed : Visibility.Visible;
			SavingThrowPanel.Visibility = (bool)swtch.IsOn ? Visibility.Visible : Visibility.Collapsed;

		}

		private void NewSpellRangeSelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void SpellListItemClick(object sender, ItemClickEventArgs e)
		{
			foreach (var spell in SpellList.Items)
			{
				if (spell != e.ClickedItem)
				{
					(spell as SpellViewModel).StopEditing.Execute(null);
				}
				else
				{
					(spell as SpellViewModel).StartEditing.Execute(null);
				}
			}
		}

		private void AttackRollDialogDoneClicked(object sender, RoutedEventArgs args)
		{
			AttackDialog.Hide();
		}

		private void RollDamageButtonClicked(object sender, RoutedEventArgs args)
		{
			AttackRollDialogPanel.Visibility = Visibility.Collapsed;
			DamageRollDialogPanel.Visibility = Visibility.Visible;
			AttackDialog.IsPrimaryButtonEnabled = false;
		}
	}
}
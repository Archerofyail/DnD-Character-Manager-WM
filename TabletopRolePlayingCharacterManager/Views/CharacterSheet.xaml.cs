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
			SheetHub.Loaded += (sender, args) =>
			{

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
			var classRace = GeneralSection.FindName("ClassAndRaceInfo") as FrameworkElement;
			classRace.Visibility = classRace.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			(GeneralSection.FindName("GeneralExpandIcon") as TextBlock).Text = classRace.Visibility == Visibility.Visible ? "-" : "+";
		}

		private void HeaderTapped(object sender, TappedRoutedEventArgs e)
		{
			var header = sender as FrameworkElement;
			if (header != null)
			{
				header.Visibility = header.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
				header.FindDescendants<TextBlock>().Where(x => x.Text == "-" || x.Text == "+");
			}
		}
		private void PhysicalDescriptionHeaderTapped(object sender, TappedRoutedEventArgs e)
		{

			//PhysicalDescription.Visibility = PhysicalDescription.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			//PhysicalDescExpandIcon.Text = PhysicalDescription.Visibility == Visibility.Visible ? "-" : "+";
		}

		private void DeleteCharacterClick(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonHideFlyout(object sender, TappedRoutedEventArgs e)
		{
			var parentButton = (sender as Button).FindAscendant<Button>();
			parentButton?.Flyout?.Hide();

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

		private void TraitListOnItemClick(object sender, ItemClickEventArgs e)
		{
			var list = sender as ListView;
			foreach (var trait in list.Items)
			{
				if (trait != e.ClickedItem)
				{
					(trait as TraitViewModel).StopEditing.Execute(null);

				}
				else
				{
					(trait as TraitViewModel).StartEditing.Execute(null);
					var traitControls = list.ContainerFromItem(trait);
					(traitControls.FindDescendantByName("DescTextBox") as TextBox)?.Focus(FocusState.Programmatic);

				}
			}
		}

		private void IsAttackSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			var chkbox = sender as ToggleSwitch;
			var statsPanel = chkbox.Parent.FindDescendantByName("NewSpellAttackStatsPanel");
			statsPanel.Visibility = (bool)chkbox.IsOn ? Visibility.Visible : Visibility.Collapsed;
		}

		private void DCSaveToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
		{
			var swtch = sender as ToggleSwitch;
			var attackPanel = swtch.Parent.FindDescendantByName("AttackRollPanel");
			var savingPanel = swtch.Parent.FindDescendantByName("SavingThrowPanel");
			attackPanel.Visibility = (bool)swtch.IsOn ? Visibility.Collapsed : Visibility.Visible;
			savingPanel.Visibility = (bool)swtch.IsOn ? Visibility.Visible : Visibility.Collapsed;

		}

		private void NewSpellRangeSelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void SpellListItemClick(object sender, ItemClickEventArgs e)
		{
			var spellList = sender as ListView;
			if (spellList != null)
			{
				foreach (var spell in spellList.Items)
				{
					if (spell != e.ClickedItem)
					{
						(spell as SpellViewModel)?.StopEditing.Execute(null);
					}
					else
					{
						(spell as SpellViewModel)?.StartEditing.Execute(null);
					}
				}
			}
		}

		private void AttackRollDialogDoneClicked(object sender, RoutedEventArgs args)
		{
			AttackRollDialogPanel.Visibility = Visibility.Visible;
			DamageRollDialogPanel.Visibility = Visibility.Collapsed;
			AttackDialog.Hide();
		}

		private void RollDamageButtonClicked(object sender, RoutedEventArgs args)
		{
			AttackRollDialogPanel.Visibility = Visibility.Collapsed;
			DamageRollDialogPanel.Visibility = Visibility.Visible;
			AttackDialog.IsPrimaryButtonEnabled = false;
		}

		private void EditClassResourceClick(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var textBlock = btn.Parent as FrameworkElement;
			if (textBlock != null)
			{
				textBlock.Visibility = Visibility.Collapsed;
			}

			var textBox = StatsSection.FindChildByName("ClassResourceTextBox");
			textBox.Visibility = Visibility.Visible;
		}

		private void EditClassResourceDoneClick(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var textBlock = btn.Parent as FrameworkElement;
			if (textBlock != null)
			{
				textBlock.Visibility = Visibility.Collapsed;
			}

			var textBox = StatsSection.FindChildByName("ClassResourceTextBlock");
			textBox.Visibility = Visibility.Visible;

		}

		private async void HitDieUsed(object sender, RoutedEventArgs e)
		{
			await HitDieDialog.ShowAsync();
		}

		private void HitDieDoneRollingClicked(object sender, RoutedEventArgs e)
		{
			HitDieDialog.Hide();
		}

		private void AttackOrSpellListLoaded(object sender, RoutedEventArgs e)
		{
			var list = sender as ListView;

			var attackButtonList = list.FindDescendants<Button>().Where(x => x.Tag != null && x.Tag.Equals("RollAttackButton"));

			foreach (var button in attackButtonList)
			{
				button.Click += ShowAttackDialogAsync;
			}

		}
	}
}
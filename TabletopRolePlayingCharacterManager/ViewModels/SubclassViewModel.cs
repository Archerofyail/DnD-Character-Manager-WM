using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class SubclassViewModel : ViewModelBase
	{
		public Subclass Subclass { get; } = new Subclass();

		public SubclassViewModel()
		{
			
		}

		public SubclassViewModel(Subclass sc)
		{
			Subclass = sc;
		}

		public string Name
		{
			get => Subclass.Name;
			set => Subclass.Name = value;
		}

		public int NumOfSkillsProficiency
		{
			get => Subclass.NumOfSkillsProficiency;
			set => Subclass.NumOfSkillsProficiency = value;
		}

		public bool EnablesMagic
		{
			get => Subclass.EnablesMagic;
			set => Subclass.EnablesMagic = value;
		}

		public int MagicStartLevel
		{
			get => Subclass.MagicStartLevel;
			set => Subclass.MagicStartLevel = value;
		}

		private ObservableCollection<PrimitiveListViewModel<string>> _skillChoices;

		public ObservableCollection<PrimitiveListViewModel<string>> SkillChoices
		{
			get
			{
				if (_skillChoices == null || _skillChoices.Count == 0)
				{
					_skillChoices = new ObservableCollection<PrimitiveListViewModel<string>>();
					foreach (var skillChoice in Subclass.SkillProficiencies)
					{
						_skillChoices.Add(new PrimitiveListViewModel<string>(skillChoice.Item1, skillChoice.Item1 > 1, skillChoice.Item2));
					}
				}

				return _skillChoices;
			}
		}

		private ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> _traits = new ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>>();
		public ObservableCollection<ChoiceViewModel<Trait, TraitViewModel>> Traits
		{
			get
			{
				if (_traits.Count == 0)
				{
					foreach (var traitChoice in Subclass.TraitsLearned)
					{
						_traits.Add(new ChoiceViewModel<Trait, TraitViewModel>(traitChoice));
					}
				}
				return _traits;
			}
		}

	}
}

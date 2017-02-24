using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyViewModel : GenericItemViewModel
	{
		private Proficiency proficiency;

		public ProficiencyViewModel(Proficiency prof)
		{
			Item = prof;
			proficiency = prof;
		}

		public ProficiencyViewModel()
		{
			proficiency = Item as Proficiency;
			if (Item == null)
			{
				proficiency = new Proficiency();
				Item = proficiency;
			}
		}

		public string Name
		{
			get { return proficiency.Name; }
			set
			{
				proficiency.Name = value;
				RaisePropertyChanged();
			}
		}
	}
}

using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyViewModel : GenericItemViewModel
	{
		private Proficiency proficiency;

		public ProficiencyViewModel(Proficiency prof)
		{
			item = prof;
			proficiency = prof;
		}

		public ProficiencyViewModel()
		{
			proficiency = item as Proficiency;
			if (item == null)
			{
				proficiency = new Proficiency();
				item = proficiency;
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

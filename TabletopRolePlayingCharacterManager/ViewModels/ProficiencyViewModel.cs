using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyViewModel : GenericItemViewModel
	{
		private Proficiency proficiency;
		public ProficiencyViewModel()
		{
			proficiency = item as Proficiency;
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

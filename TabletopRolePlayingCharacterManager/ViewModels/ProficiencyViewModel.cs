using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyViewModel : GenericItemViewModel
	{
		//private Proficiency proficiency;
		public override object Item { get; set; } = new Proficiency();
		public ProficiencyViewModel(Proficiency prof)
		{
			Item = prof;
			
		}

		public ProficiencyViewModel()
		{
			//Item = new Proficiency();
		}

		public string Name
		{
			get { return ((Proficiency)Item).Name; }
			set
			{
				((Proficiency)Item).Name = value;
				RaisePropertyChanged();
			}
		}
	}
}

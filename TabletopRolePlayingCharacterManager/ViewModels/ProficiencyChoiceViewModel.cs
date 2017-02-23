using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyChoiceViewModel : ChoiceViewModel<Proficiency, ProficiencyViewModel>
	{
		private ProficiencyType type;
		
		public ProficiencyChoiceViewModel(List<Proficiency> profs, int totalBonus, bool canSelectMultiple, ProficiencyType type) : base(profs)
		{
			this.type = type;
			foreach (var prof in profs)
			{
				prof.Type = type;
			}
		}

		protected override void AddItemExec()
		{

			var newProf = new Proficiency {Type = type};
			AddModifiedItem(newProf);
		}
	}
}

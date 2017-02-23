using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyChoiceViewModel : ChoiceViewModel<Proficiency, ProficiencyViewModel>
	{
		private ProficiencyType type;

		public ProficiencyChoiceViewModel(ProficiencyType type, List<Proficiency> profs = null, int totalBonus = 1, bool canSelectMultiple = false) : base(profs)
		{
			this.type = type;
			if (profs != null)
			{
				
				foreach (var prof in profs)
				{
					prof.Type = type;
					Choices.Add(new ProficiencyViewModel(prof));
				}
			
			}
		}

		protected override void AddItemExec()
		{

			var newProf = new Proficiency { Type = type };
			AddModifiedItem(newProf);
		}
	}
}

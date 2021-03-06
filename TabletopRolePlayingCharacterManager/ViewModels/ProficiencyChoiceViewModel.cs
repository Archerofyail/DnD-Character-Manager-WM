﻿using System.Collections.Generic;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.ViewModels
{
	public class ProficiencyChoiceViewModel : ChoiceViewModel<Proficiency, ProficiencyViewModel>
	{
		private ProficiencyType _type;


		public ProficiencyChoiceViewModel(ProficiencyType type, List<Proficiency> profs = null, int totalBonus = 1, bool canSelectMultiple = false)
		{
			this._type = type;
			if (profs != null)
			{
				
				foreach (var prof in profs)
				{
					prof.Type = type;
					
				}
				Items.AddRange(profs);
			
			}
		}

		protected override void AddItemExec()
		{

			var newProf = new Proficiency { Type = _type };
			AddModifiedItem(newProf);
		}
	}
}

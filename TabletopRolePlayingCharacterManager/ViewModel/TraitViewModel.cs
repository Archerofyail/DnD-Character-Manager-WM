using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	public class TraitViewModel
	{
		private Trait trait;
		public TraitViewModel(Trait trait)
		{
			this.trait= trait;
		}

		public string Trait
		{
			get { return trait.Description; }
			set { trait.Description = value; }
		}
	}
}

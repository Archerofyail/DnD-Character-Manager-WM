using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TabletopRolePlayingCharacterManager.Types;

namespace TabletopRolePlayingCharacterManager.ViewModel
{
	//TODO: Figure out how to update the class and race libraries if the names are changed
	public class ClassViewModel : ViewModelBase
	{

		private ClassBonus classBonus;

		public ClassViewModel(ClassBonus cl)
		{
			classBonus = cl;
		}

		public string Name
		{
			get { return classBonus.ClassName; }
			set
			{
				classBonus.ClassName = value;
				RaisePropertyChanged();
			}
		}



		public string ClassBonusesString
		{
			get
			{
				var text = "";
				foreach (var levelTrait in classBonus.TraitsByLevel)
				{
					text += "Level " + levelTrait.Key + ":\n";
					foreach (var traitList in levelTrait.Value)
					{
						foreach (var trait in traitList)
						{
							text += trait.Description + "\n";
						}
						
					}
				}
				return text;
			}
		}
	}
}

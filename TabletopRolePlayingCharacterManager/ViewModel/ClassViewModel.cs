﻿using System;
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

		private string name;
		private ClassBonus classBonus;

		public ClassViewModel(string name, ClassBonus cl)
		{
			this.name = name;
			classBonus = cl;
		}

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
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
					text += "Level " + levelTrait.Item1 + ":\n";
					foreach (var trait in levelTrait.Item2)
					{
						text += trait.Description + "\n";
					}
				}
				return text;
			}
		}
	}
}
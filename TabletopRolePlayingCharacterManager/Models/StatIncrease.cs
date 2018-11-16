using System;
using System.Linq;
using System.Reflection;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class StatIncrease
	{
		protected Character5E Character { get; set; }
		private static string _bonusName { get; set; }
		public string BonusName => _bonusName;

		public int Bonus { get; set; }

		public virtual void AddBonus() { }

		public virtual void RemoveBonus() { }

		public static StatIncrease GetNewByName(string name)
		{
			
			var statIncType = typeof(StatIncrease);
			var wantedType = statIncType.GetTypeInfo()
				.Assembly.GetTypes()
				.First((type) =>
				{
					var typeInfo = type.GetTypeInfo();
					return typeInfo.IsClass && typeInfo.IsSubclassOf(statIncType) &&
						   (string)typeInfo.GetDeclaredProperty("bonusName").GetValue(null) == name;
				});

			return Activator.CreateInstance(wantedType) as StatIncrease;

		}
	}
}

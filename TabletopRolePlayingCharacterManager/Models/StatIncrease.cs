namespace TabletopRolePlayingCharacterManager.Models
{
	public class StatIncrease
	{
		protected Character5E Character { get; set; }
		private static string bonusName { get; set; }

		public string BonusName
		{
			get { return bonusName; }
		}

		public int Bonus { get; set; }

		public virtual void AddBonus()
		{
			
		}

		public virtual void RemoveBonus()
		{
			
		}
	}
}

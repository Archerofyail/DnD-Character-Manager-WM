namespace TabletopRolePlayingCharacterManager.Types
{

	public class CharacterTrait
	{
		public string Description { get; private set; }
		public string Title { get; private set; }

		public CharacterTrait()
		{
			Title = "";
			Description = "";
		}

		public CharacterTrait(string title, string description = "")
		{
			Title = title;
			Description = description;
		}

		public void UpdateTrait(string title, string description)
		{
			Title = title;
			Description = description;
		}
	}

	
}

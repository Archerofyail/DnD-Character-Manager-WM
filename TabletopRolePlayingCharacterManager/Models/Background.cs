using System.Collections.Generic;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class Background
	{
		public string BackgroundFeatureName { get; set; }
		public List<Skill> Skills { get; set; } = new List<Skill>();
		public List<List<Item>> Equipment { get; set; } = new List<List<Item>>();
		public List<List<string>> Proficiencies { get; set; } = new List<List<string>>();

		public List<string> BackgroundFeatures { get; set; } = new List<string>();
		public List<string> Ideals { get; set; } = new List<string>();
		public List<string> Bonds { get; set; } = new List<string>();
		public List<string> Flaws { get; set; } = new List<string>();
		public List<Trait> Traits { get; set; } = new List<Trait>();

	}
}

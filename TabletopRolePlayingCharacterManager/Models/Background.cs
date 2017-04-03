using System.Collections.Generic;
using Windows.UI.Xaml;

namespace TabletopRolePlayingCharacterManager.Models
{
	public class Background
	{
		public string Name { get; set; }
		public string BackgroundFeatureName { get; set; }
		public List<string> Skills { get; set; } = new List<string>();
		public List<List<Item>> Equipment { get; set; } = new List<List<Item>>();
		public List<List<Proficiency>> Proficiencies { get; set; } = new List<List<Proficiency>>();

		public List<string> BackgroundFeatures { get; set; } = new List<string>();
		public List<string> Ideals { get; set; } = new List<string>();
		public List<string> Bonds { get; set; } = new List<string>();
		public List<string> Flaws { get; set; } = new List<string>();
		public List<Trait> Traits { get; set; } = new List<Trait>();

	}
}

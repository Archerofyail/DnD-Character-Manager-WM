using System.Diagnostics;

namespace TabletopRolePlayingCharacterManager.Types
{
	public class ItemPair <T, TK>
	{
		public T Item1 { get; set; }
		public TK Item2 { get; set; }

		public ItemPair()
		{
			
		}

		public ItemPair(T item1, TK item2)
		{
			Item1 = item1;
			Item2 = item2;
		} 
	}

	public class StringPair
	{
		private string item1;
		private string item2;
		public string Item1
		{
			get => item1;
			set => item1 = value ?? "";
		}
		public string Item2
		{
			get => item2;
			set => item2 = value ?? "";
		}
		public StringPair()
		{
			Debug.WriteLine("Stringpair made");
		}
	}
}

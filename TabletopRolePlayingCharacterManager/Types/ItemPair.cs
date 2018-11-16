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
		private string _item1;
		private string _item2;
		public string Item1
		{
			get => _item1;
			set => _item1 = value ?? "";
		}
		public string Item2
		{
			get => _item2;
			set => _item2 = value ?? "";
		}
		public StringPair()
		{
			Debug.WriteLine("Stringpair made");
		}
	}
}

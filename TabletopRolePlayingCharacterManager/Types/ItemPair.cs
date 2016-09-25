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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Character_Manager.Types
{
	class ItemPair <T, K>
	{
		public T Item1 { get; set; }
		public K Item2 { get; set; }

		public ItemPair()
		{
			
		}

		public ItemPair(T item1, K item2)
		{
			Item1 = item1;
			Item2 = item2;
		} 
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;

namespace TabletopRolePlayingCharacterManager.Types
{
	public static class CharacterManager
	{
		public static Character5E CurrentCharacter { get; set; }
		public static List<Character5E> Characters { get; private set; }
		public async static void SaveNewCharacter()
		{

		}

		public async static void LoadAllCharacters()
		{

		}
	}
}

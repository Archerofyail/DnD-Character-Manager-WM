using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopRolePlayingCharacterManager.Models;
using Windows.Storage;


namespace TabletopRolePlayingCharacterManager.Types
{
	public static class CharacterManager
	{
		public static Character5E CurrentCharacter { get; set; }
		public static List<Character5E> Characters { get; private set; }
		private static StorageFolder SaveFolder { get; set; }

		static CharacterManager()
		{
			SaveFolder = ApplicationData.Current.RoamingFolder.CreateFolderAsync("Characters").GetResults();
			LoadAllCharacters();
		}

		public async static void LoadAllCharacters()
		{
			foreach(var file in await SaveFolder.GetFilesAsync())
			{
				if (file.FileType.ToLower().Contains("json"))
				{
					Characters.Add(JsonConvert.DeserializeObject<Character5E>(await FileIO.ReadTextAsync(file)));
				}
			}
		}

		public static Character5E GetNewChar()
		{
			var character = new Character5E(Characters.Count);
			Characters.Add(character);
			CurrentCharacter = character;
			return character;
		}

		internal async static void SaveCurrentCharacter()
		{
			var json = JsonConvert.SerializeObject(CurrentCharacter);
			var saveFile = await SaveFolder.CreateFileAsync(CurrentCharacter.id + " - " + CurrentCharacter.Name + ".json");
			await FileIO.WriteTextAsync(saveFile, json);
		}
	}
}

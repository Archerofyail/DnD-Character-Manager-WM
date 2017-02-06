using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using TabletopRolePlayingCharacterManager.Models;
using Windows.Storage;


namespace TabletopRolePlayingCharacterManager.Types
{
	public static class CharacterManager
	{
		public static Character5E CurrentCharacter { get; set; }
		public static List<Character5E> Characters { get; private set; } = new List<Character5E>();
		private static StorageFolder SaveFolder { get; set; }

		static CharacterManager()
		{
			var savefolder = ApplicationData.Current.RoamingFolder.TryGetItemAsync("Characters").GetResults();
			if (savefolder != null)
			{

				SaveFolder = ApplicationData.Current.RoamingFolder.GetFolderAsync("Characters").GetResults();

			}
			


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
			var saveName = CurrentCharacter.id + " - " + CurrentCharacter.Name + ".json";
			var json = JsonConvert.SerializeObject(CurrentCharacter);
			var potsave = await SaveFolder.TryGetItemAsync(saveName);
			StorageFile saveFile;
			if (potsave != null)
			{
				saveFile = await SaveFolder.GetFileAsync(saveName);
				await FileIO.WriteTextAsync(saveFile, json);
				
			}
			else
			{
				saveFile = await SaveFolder.CreateFileAsync(saveName);
				await FileIO.WriteTextAsync(saveFile, json);
			}
			
		}
	}
}

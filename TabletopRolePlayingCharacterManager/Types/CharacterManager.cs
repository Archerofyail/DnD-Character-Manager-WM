using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private static List<Character5E> characters=new List<Character5E>();
		public static List<Character5E> Characters
		{
			get
			{
				
				return characters;
			}
			private set { characters = value; }
		}

		private static StorageFolder SaveFolder { get; set; }


		public static async Task SetSaveFolder()
		{
			var savefolder = await ApplicationData.Current.RoamingFolder.TryGetItemAsync("Characters");
			if (savefolder != null)
			{

				SaveFolder = await ApplicationData.Current.RoamingFolder.GetFolderAsync("Characters");
				
			}
			else
			{
				SaveFolder = await ApplicationData.Current.RoamingFolder.CreateFolderAsync("Characters");
			}
			Debug.WriteLine("Roaming Folder is at: " + SaveFolder.Path);
		}

		public static async Task LoadAllCharacters()
		{
			if (SaveFolder == null)
			{
				await SetSaveFolder();
			}
			foreach(var file in await SaveFolder.GetFilesAsync())
			{
				if (file.FileType.ToLower().Contains("json"))
				{
					characters.Add(JsonConvert.DeserializeObject<Character5E>(await FileIO.ReadTextAsync(file)));
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
			var saveName = CurrentCharacter.id + ".json";
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

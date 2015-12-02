using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DnD_Character_Manager.Types
{
	public static class JsonLoader
	{
		//Loads a file with the specified file name, from the specified folder. Returns an empty string if the file wasn't found
		public static async Task<string> LoadJsonFromFile(string fileName, StorageFolder folder)
		{
			string json = "";

			StorageFile file = await folder.TryGetItemAsync(fileName) as StorageFile;
			if (file != null)
			{
				json = await FileIO.ReadTextAsync(file);
			}
			return json;
		}
	}
}

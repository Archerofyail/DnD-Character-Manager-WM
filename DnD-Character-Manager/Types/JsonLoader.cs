using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;

namespace DnD_Character_Manager.Types
{
	public static class JsonLoader
	{
		public static ResourceLoader resourceLoader;
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

		public async static Task<string> LoadJsonFromEmbeddedResource(string resourceName)
		{
			//var resourceNamesFound = typeof (JsonLoader).GetTypeInfo().Assembly.GetManifestResourceNames();
			var json = "";
			using (var fileStream = typeof (JsonLoader).GetTypeInfo().Assembly.GetManifestResourceStream("DnD_Character_Manager.Assets.Data." + resourceName))
			{
				using (var stream = new StreamReader(fileStream))
				{
					json = stream.ReadToEnd();
				}
			}
			return json;
		}
	}
}

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
using SQLite;

namespace DnD_Character_Manager.Types
{
	public static class JsonLoader
	{
		public static ResourceLoader resourceLoader;
		public static SQLiteAsyncConnection dbConnection;

		static JsonLoader()
		{
			dbConnection = new SQLiteAsyncConnection(Path.Combine(ApplicationData.Current.RoamingFolder.Path,"ItemDB.db"));
			dbConnection.CreateTableAsync<Skill>();
			dbConnection.CreateTableAsync<Race>();
			Debug.WriteLine("db created ");
		}

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
			
			var json = "";
			await Task.Run(() =>
			{
				Debug.WriteLine("looking for " + resourceName + ".json");
				var Assembly = typeof(JsonLoader).GetTypeInfo().Assembly;
				var resourceList = Assembly.GetManifestResourceNames();
				foreach (var s in resourceList)
				{
					Debug.WriteLine(s);
				}
				
				try
				{
					Debug.WriteLine("Loading Stream...");
					using (
						var stream =
							typeof(JsonLoader).GetTypeInfo()
								.Assembly.GetManifestResourceStream("DnD_Character_Manager.Assets.Data." + resourceName + ".json"))
					{
						Debug.WriteLine("Loaded stream, value is " + stream + " grabbing json...");

						using (var textStream = new StreamReader(stream))
						{
							Debug.WriteLine("Loaded StreamReader with value " + textStream);
							json = textStream.ReadToEnd();
							Debug.WriteLine("grabbed json woth value \n" + json);
						}
					}
				}
				catch (Exception e)
				{
					Debug.WriteLine("Exception happened:\n" + e.StackTrace);
					Debug.WriteLine(e.Message);
				}
			});
			
			return json;
		}
	}
}

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
using SQLite.Net;

namespace TabletopRolePlayingCharacterManager.Types
{
	public static class DBLoader
	{
		public static ResourceLoader resourceLoader;
		public static SQLiteConnection dbConnection;

		static DBLoader()
		{
            CreatePreexistingData();
			Debug.WriteLine("db created ");
		}

        public static void CreatePreexistingData()
        {
            dbConnection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), Path.Combine(ApplicationData.Current.RoamingFolder.Path, "ItemDB.db"));
            dbConnection.CreateTable<Skill>();
            dbConnection.CreateTable<Race>();
            dbConnection.CreateTable<string>();
        }

		//Loads a file with the specified file name, from the speci fied folder. Returns an empty string if the file wasn't found
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
				//Debug.WriteLine("looking for " + resourceName + ".json");
				var assembly = typeof(DBLoader).GetTypeInfo().Assembly;
				
				
				try
				{
					//Debug.WriteLine("Loading Stream...");
					using (
						var stream =
							assembly.GetManifestResourceStream("TabletopRolePlayingCharacterManager.Assets.Data." + resourceName + ".json"))
					{
						//Debug.WriteLine("Loaded stream, value is " + stream + " grabbing json...");

						using (var textStream = new StreamReader(stream))
						{
							//Debug.WriteLine("Loaded StreamReader with value " + textStream);
							json = textStream.ReadToEnd();
							//Debug.WriteLine("grabbed json woth value \n" + json);
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

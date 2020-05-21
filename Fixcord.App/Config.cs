using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fixcord.App
{
	public static class Configuration
    {
        private static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Fixcord\\";
        public static readonly string ConfigFile = Path + "config.json";
        public static Config Config = new Config();

        public static async void Save()
        {
            Directory.CreateDirectory(Path);
            using (StreamWriter outputFile = new StreamWriter(ConfigFile))
                await outputFile.WriteAsync(JsonSerializer.Serialize(Config));
        }

        public static async Task Load()
        {
            try
            {
                StreamReader sr = new StreamReader(ConfigFile);
                string file = await sr.ReadToEndAsync();
                sr.Close();

                Config = JsonSerializer.Deserialize<Config>(file)!;
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class Config
    {
        public string? Token { get; set; }
    }
}

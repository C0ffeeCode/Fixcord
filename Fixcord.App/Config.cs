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
        public static readonly string file = "config.json";
        public static Config Config = new Config();

        public static async void Save()
        {
            var b = JsonSerializer.Serialize(Config);

            using (StreamWriter outputFile = new StreamWriter(file))
            {
                await outputFile.WriteAsync(b);
            }
        }

        /// <summary>
        /// Loads the config to Config
        /// </summary>
        /// <returns>Returns true on error (e.g. the file was not found)</returns>
        public static async Task<bool> Load()
        {
            try
            {
                StreamReader sr = new StreamReader("config.json");
                string file = await sr.ReadToEndAsync();
                sr.Close();

                Config = JsonSerializer.Deserialize<Config>(file)!;
                return false;
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine(ex.Message);
                return true;
            }
        }
    }

    public class Config
    {
        public string? Token { get; set; }
    }
}

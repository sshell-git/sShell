using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sShell.Classes
{
    public class SettingsFile
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("favorites")]
        public KeyValuePair<string, Favorite> Favorites { get; set; }
        [JsonProperty("initSuccess")]
        public bool InitSuccess { get; private set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
    }

    public class Favorite
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("path")]
        public string FolderPath { get; set; }
        [JsonProperty("category")]
        public Category Category { get; set; }
    }

    public class Theme
    {

    }

    public enum Category
    {
        Generic,
        Media,
        Social,
        GamesAndApps,
        Tools,
        Misc
    }

    public class SettingsHandler
    {
        // define variables
        public string FolderPath { get; private set; }
        public SettingsFile CurrentSettingsFile { get; private set; }
        public double CurrentVersion { get; }
        public string UseLang { get; set; }
        // init the variables
        public bool Initialize()
        {
            UseLang = "en_US";
            // folderpath is always empty at the start
            if (string.IsNullOrEmpty(FolderPath))
            {
                // init folderpath with the correct path (e.g. %AppData%\sShell\)
                FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"sShell");
            }
            // trycatch since there may be errors
            try
            {
                // case switch because why not
                switch (Directory.Exists(FolderPath))
                {
                    case true:
                        return true;
                    case false:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // load the settings file
        public bool LoadSettings()
        {
            if (Initialize())
            {
                try
                {
                    if (File.Exists(Path.Combine(FolderPath + "settings.json")))
                    {
                        string Text = File.ReadAllText(Path.Combine(FolderPath + "settings.json"));
                        CurrentSettingsFile = JsonConvert.DeserializeObject(Text) as SettingsFile;
                        return true;
                    }
                    else
                    {
                        CurrentSettingsFile = new()
                        {
                            Version = "0.3b"
                        };
                        string json = JsonConvert.SerializeObject(CurrentSettingsFile, Formatting.Indented);
                        MessageBox.Show(json);
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

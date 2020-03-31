using System.Reflection;

namespace Core.Config
{
    using System.IO;
    using System.Text.Json;
    using System;
    using Models;

    public class Configuration : IConfiguration
    {
        private readonly string _settingsFile;
        private readonly string _assetsPath;
        
        public ApplicationSettings Settings { get; }
        public ContentPath ContentPath { get; }
        
        public Configuration()
        {
            _settingsFile = Path.Combine(Environment.CurrentDirectory, "Settings.json");
            _assetsPath = Path.Combine(Environment.CurrentDirectory, "Assets");
            
            Settings = LoadSettings(_settingsFile);
            ContentPath = new ContentPath(_assetsPath);
        }

        private ApplicationSettings LoadSettings(string path)
        {
            var settings = JsonSerializer.Deserialize<ApplicationSettings>(File.ReadAllText(path));
            settings.Info.WindowTitle = $"{settings.Info.ProgramName} v.{settings.Info.ProgramVersion}";
            return settings;
        }

        public void SaveSettings()
        {
            string settingsJson = JsonSerializer.Serialize(Settings);
            File.WriteAllText(_settingsFile, settingsJson);
        }

    }
}
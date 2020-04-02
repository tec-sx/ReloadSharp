namespace Core.Config
{
    using System.IO;
    using System.Text.Json;
    using System;

    public static class Configuration
    {
        public static ContentPath ContentPath { get; private set; }
        public static ApplicationSettings Settings { get; private set; }

        public static void Init()
        {
            var settingsFile = Path.Combine(Environment.CurrentDirectory, "Settings.json");
            
            ContentPath = new ContentPath(Path.Combine(Environment.CurrentDirectory, "Assets"));
            Settings = LoadSettings<ApplicationSettings>(settingsFile);
        }

        public static T LoadSettings<T>(string file)
        {
            var settings = JsonSerializer.Deserialize<T>(File.ReadAllText(file));
            return settings;
        }

        public static void SaveSettings(string file)
        {
            var settingsJson = JsonSerializer.Serialize(Settings);
            File.WriteAllText(file, settingsJson);
        }
    }
}
namespace Core.Config
{
    using System.IO;
    using System.Text.Json;
    using System;

    public static class Configuration
    {
        private static readonly string SettingsFile = Path.Combine(Environment.CurrentDirectory, "settings.json");

        public static ContentPath ContentPath { get; } = new ContentPath(Path.Combine(Environment.CurrentDirectory, "Assets"));
        public static ApplicationSettings Settings { get; private set; }

        public static void LoadDefaultConfiguration()
        {
            Settings = LoadSettings<ApplicationSettings>();
        }

        public static T LoadSettings<T>()
        {
            var settings = JsonSerializer.Deserialize<T>(File.ReadAllText(SettingsFile));
            return settings;
        }

        public static void SaveSettings()
        {
            var settingsJson = JsonSerializer.Serialize(Settings);
            File.WriteAllText(SettingsFile, settingsJson);
        }
    }
}
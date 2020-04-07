using System.Drawing;
using Silk.NET.Windowing.Common;

namespace Core.Config
{
    using System.IO;
    using System.Text.Json;
    using System;

    public static class Configuration
    {
        private static readonly string SettingsFile = Path.Combine(Environment.CurrentDirectory, "Settings.json");
        public static ContentPath ContentPath { get; private set; }
        public static ApplicationSettings Settings { get; private set; }

        public static void LoadDefaultConfiguration()
        {
            ContentPath = new ContentPath(Path.Combine(Environment.CurrentDirectory, "Assets"));
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
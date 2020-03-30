namespace Core.Config
{
    using System.IO;
    using System.Text.Json;
    using System;
    using Models;

    public class Configuration : IConfiguration
    {
        private readonly string _settingsFile;
        private readonly ApplicationSettings _settings;
        
        public Configuration()
        {
            _settingsFile = Path.Combine($"{Environment.CurrentDirectory}", "Assets/Settings/Settings.json");
            _settings = JsonSerializer.Deserialize<ApplicationSettings>(File.ReadAllText(_settingsFile));
            _settings.Info.WindowTitle = $"{_settings.Info.ProgramName} v.{_settings.Info.ProgramVersion}";
        }

        public void SaveSettings()
        {
            string settingsJson = JsonSerializer.Serialize(_settings);
            File.WriteAllText(_settingsFile, settingsJson);
        }

        public ApplicationSettings GetSettings()
        {
            return _settings;
        }
        
    }
}
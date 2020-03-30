namespace Core.Config.Models
{
    using System.Text.Json.Serialization;
    using Raylib_cs;

    public class DisplaySettings
    {
        [JsonPropertyName("Width")] public int Width { get; set; }
        [JsonPropertyName("Height")] public int Height { get; set; }
        [JsonPropertyName("TargetFps")] public int TargetFps { get; set; }
        [JsonPropertyName("Flags")] public ConfigFlag[] Flags { get; set; }
    }

    public class AudioSettings
    {
        [JsonPropertyName("SampleRate")] public int SampleRate { get; set; }
        [JsonPropertyName("SampleSize")] public int SampleSize { get; set; }
        [JsonPropertyName("Channels")] public int Channels { get; set; }
        [JsonPropertyName("MasterVolume")] public float MasterVolume { get; set; }
    }

    public class ApplicationInfo
    {
        [JsonPropertyName("ProgramName")] public string ProgramName { get; set; }
        [JsonPropertyName("ProgramVersion")] public string ProgramVersion { get; set; }
        public string WindowTitle { get; set; }
    }

    public class ApplicationSettings
    {
        [JsonPropertyName("ApplicationInfo")] public ApplicationInfo Info { get; set; }
        [JsonPropertyName("DisplaySettings")] public DisplaySettings Display { get; set; }
        [JsonPropertyName("AudioSettings")] public AudioSettings Audio { get; set;}
    }
    
}
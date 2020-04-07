namespace Core.Config
{
    using CoreSystem;
    using System.Text.Json.Serialization;


    public class DisplaySettings
    {
        [JsonPropertyName("Width")] public int Width { get; set; }
        [JsonPropertyName("Height")] public int Height { get; set; }
        [JsonPropertyName("TargetFps")] public int TargetFps { get; set; }
        [JsonPropertyName("UseVulkan")] public bool UseVulkan { get; set; }
        [JsonPropertyName("IsFullScreen")] public bool IsFullScreen { get; set; }
        [JsonPropertyName("VSync")] public bool VSync { get; set; }
        [JsonPropertyName("RefreshRate")] public int RefreshRate { get; set; }
    }

    public class AudioSettings
    {
        [JsonPropertyName("SampleRate")] public int SampleRate { get; set; }
        [JsonPropertyName("SampleSize")] public int SampleSize { get; set; }
        [JsonPropertyName("Channels")] public int Channels { get; set; }
        [JsonPropertyName("MasterVolume")] public float MasterVolume { get; set; }
        [JsonPropertyName("Format")] public string Format { get; set; }
    }

    public class ImageSettings
    {
        [JsonPropertyName("Format")] public string Format { get; set; }
    }

    public class ModelSettings
    {
        [JsonPropertyName("Format")] public string Format { get; set; }
    }

    public class ApplicationInfo
    {
        [JsonPropertyName("ProgramName")] public string ProgramName { get; set; }
        [JsonPropertyName("ProgramVersion")] public string ProgramVersion { get; set; }
    }

    public class Flags
    {
        [JsonPropertyName("Debug")] public bool Debug { get; set; }
    }
    
    public class ApplicationSettings
    {
        [JsonPropertyName("ApplicationInfo")] public ApplicationInfo Info { get; set; }
        [JsonPropertyName("DisplaySettings")] public DisplaySettings Display { get; set; }
        [JsonPropertyName("AudioSettings")] public AudioSettings Audio { get; set;}
        [JsonPropertyName(("ImageSettings"))] public ImageSettings Image { get; set; }
        [JsonPropertyName("ModelSettings")] public ModelSettings Model { get; set; }
        [JsonPropertyName("Flags")] public Flags Flags { get; set; }
    }
}
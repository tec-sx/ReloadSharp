namespace Engine.Configuration
{
    using Engine.AssetPipeline;
    using Engine.Configuration.Extensions;
    using Engine.Graphics;

    public class ConfigurationManager : IConfigurationManager
    {
        private readonly ContentPaths contentPaths;
        private readonly SystemConfiguration systemConfiguration;
        private readonly UserConfiguration userConfiguration;

        public ConfigurationManager()
        {
            contentPaths = new ContentPaths();
            systemConfiguration = new SystemConfiguration();
            userConfiguration = new UserConfiguration();

            userConfiguration.Load();
        }

        public DisplayConfiguration CreateDisplayConfiguration()
        {
            return new DisplayConfiguration
            {
                Resolution = userConfiguration.DisplayResolution,
                RefreshRate = userConfiguration.DisplayRefreshRate,
                TargetFps = systemConfiguration.TargetFps,
                InFullScreen = userConfiguration.DisplayInFullScreen,
                EnableVSync = userConfiguration.DisplayEnableVsync,
                EnableVulkan = userConfiguration.DisplayEnableVulkan,
                WindowTitle = $"{systemConfiguration.ProgramName} - v.{systemConfiguration.ProgramVersion}",
            };
        }


        public AssetsConfiguration CreateAssetsConfiguration()
        {
            return new AssetsConfiguration
            {
                SoundsPath = contentPaths.Sounds,
                MusicPath = contentPaths.Music,
                ModelsPath = contentPaths.Models,
                TexturesPath = contentPaths.Textures,
                ImageFormat = systemConfiguration.ImageExtension,
                TextureFormat = systemConfiguration.TextureExtension,
                SoundFormat = systemConfiguration.AudioExtension,
                ModelFormat = systemConfiguration.ModelExtension
            };
        }
    }
}

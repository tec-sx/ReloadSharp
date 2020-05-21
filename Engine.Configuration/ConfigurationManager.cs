namespace Engine.Configuration
{
    using Engine.AssetPipeline;
    using Engine.Configuration.Extensions;
    using Engine.Graphics;
    using Reload.Input.Configuration;

    public class ConfigurationManager : IConfigurationManager
    {
        private readonly UserConfiguration userConfiguration;

        public ConfigurationManager()
        {
            userConfiguration = new UserConfiguration();
            userConfiguration.Load();
        }

        public DisplayConfiguration CreateDisplayConfiguration()
        {
            return new DisplayConfiguration
            {
                Resolution = userConfiguration.DisplayResolution,
                RefreshRate = userConfiguration.DisplayRefreshRate,
                TargetFps = SystemConfiguration.TargetFps,
                InFullScreen = userConfiguration.DisplayInFullScreen,
                EnableVSync = userConfiguration.DisplayEnableVsync,
                EnableVulkan = userConfiguration.DisplayEnableVulkan,
                WindowTitle = $"{SystemConfiguration.ProgramName} - v.{SystemConfiguration.ProgramVersion}",
            };
        }


        public AssetsConfiguration CreateAssetsConfiguration()
        {
            return new AssetsConfiguration
            {
                SoundsPath = ContentPaths.Sounds,
                MusicPath = ContentPaths.Music,
                ModelsPath = ContentPaths.Models,
                TexturesPath = ContentPaths.Textures,
                ImageFormat = SystemConfiguration.ImageExtension,
                TextureFormat = SystemConfiguration.TextureExtension,
                SoundFormat = SystemConfiguration.AudioExtension,
                ModelFormat = SystemConfiguration.ModelExtension
            };
        }

        public InputConfiguration CreateKeyboardConfiguration()
        {
            return new InputConfiguration
            {
                KeyboardId = userConfiguration.KeyboardId,
                Up = userConfiguration.KeyUp,
                Down = userConfiguration.KeyDown,
                Left = userConfiguration.KeyLeft,
                Right = userConfiguration.KeyRight,
                Run = userConfiguration.KeyRun,
                Duck = userConfiguration.KeyDuck,
                Jump = userConfiguration.KeyJump,
                OpenInventory = userConfiguration.KeyOpenInventory,
                ToggleFightMode = userConfiguration.KeyToggleFightMode,
                Select = userConfiguration.KeySelect,
                Pause = userConfiguration.KeyPause,

                MouseId = userConfiguration.MouseId,
                Interact = userConfiguration.MouseInteract
            };
        }
    }
}

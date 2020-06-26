namespace Reload.Configuration
{
    using Reload.Assets;
    using Reload.Configuration.Extensions;
    using Reload.Input.Configuration;
    using Reload.Graphics;
    using System.Drawing;
    using Silk.NET.Windowing.Common;

    public class ConfigurationManager
    {
        private readonly UserConfiguration _userConfiguration;

        public ConfigurationManager()
        {
            _userConfiguration = new UserConfiguration();
            _userConfiguration.Load();
        }

        public DisplayConfiguration CreateDefaultDisplayConfiguration()
        {
            return new DisplayConfiguration
            {
                Resolution = _userConfiguration.DisplayResolution,
                RefreshRate = _userConfiguration.DisplayRefreshRate,
                TargetFps = SystemConfiguration.TargetFps,
                InFullScreen = _userConfiguration.DisplayInFullScreen,
                EnableVSync = _userConfiguration.DisplayEnableVsync,
                EnableVulkan = _userConfiguration.DisplayEnableVulkan,
                WindowTitle = $"{SystemConfiguration.ProgramName} - v.{SystemConfiguration.ProgramVersion}",
                WindowBorder = WindowBorder.Fixed,
                Position = new Point(100, 100)
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
                KeyboardId = _userConfiguration.KeyboardId,
                Up = _userConfiguration.KeyUp,
                Down = _userConfiguration.KeyDown,
                Left = _userConfiguration.KeyLeft,
                Right = _userConfiguration.KeyRight,
                Run = _userConfiguration.KeyRun,
                Duck = _userConfiguration.KeyDuck,
                Jump = _userConfiguration.KeyJump,
                OpenInventory = _userConfiguration.KeyOpenInventory,
                ToggleFightMode = _userConfiguration.KeyToggleFightMode,
                Select = _userConfiguration.KeySelect,
                Pause = _userConfiguration.KeyPause,

                MouseId = _userConfiguration.MouseId,
                Interact = _userConfiguration.MouseInteract
            };
        }
    }
}

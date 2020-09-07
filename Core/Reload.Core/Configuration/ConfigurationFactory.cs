using Reload.Core.Windowing;
using System.Drawing;

namespace Reload.Core.Configuration
{
    public static class ConfigurationFactory
    {
        //public static void Initialize()
        //{
        //    _userConfiguration = new UserConfiguration().Load();
        //}

        public static DisplayConfiguration CreateDefaultDisplayConfiguration()
        {
            return new DisplayConfiguration
            {
                Resolution = new Size(1280, 768),
                RefreshRate = 60,
                TargetFps = 60,
                InFullScreen = false,
                EnableVSync = true,
                WindowTitle = $"{SystemConfiguration.ProgramName} - v.{SystemConfiguration.ProgramVersion}",
                WindowBorder = WindowBorder.Fixed,
                Position = new Point(100, 100)
            };
        }

        public static SystemConfiguration CreateDefault()
        {
            return new SystemConfiguration
            {
                Display = new DisplayConfiguration
                {
                    Resolution = new Size(1280, 768),
                    RefreshRate = 60,
                    TargetFps = 60,
                    InFullScreen = false,
                    EnableVSync = true,
                    WindowTitle = $"{SystemConfiguration.ProgramName} - v.{SystemConfiguration.ProgramVersion}",
                    WindowBorder = WindowBorder.Fixed,
                    Position = new Point(100, 100)
                }
            };
        }


        //public static AssetsConfiguration CreateAssetsConfiguration()
        //{
        //    return new AssetsConfiguration
        //    {
        //        SoundsPath = ContentPaths.Sounds,
        //        MusicPath = ContentPaths.Music,
        //        ModelsPath = ContentPaths.Models,
        //        TexturesPath = ContentPaths.Textures,
        //        ImageFormat = SystemConfiguration.ImageExtension,
        //        TextureFormat = SystemConfiguration.TextureExtension,
        //        SoundFormat = SystemConfiguration.AudioExtension,
        //        ModelFormat = SystemConfiguration.ModelExtension
        //    };
        //}

        //public static InputConfiguration CreateKeyboardConfiguration()
        //{
        //    return new InputConfiguration
        //    {
        //        KeyboardId = _userConfiguration.KeyboardId,
        //        Up = _userConfiguration.KeyUp,
        //        Down = _userConfiguration.KeyDown,
        //        Left = _userConfiguration.KeyLeft,
        //        Right = _userConfiguration.KeyRight,
        //        Run = _userConfiguration.KeyRun,
        //        Duck = _userConfiguration.KeyDuck,
        //        Jump = _userConfiguration.KeyJump,
        //        OpenInventory = _userConfiguration.KeyOpenInventory,
        //        ToggleFightMode = _userConfiguration.KeyToggleFightMode,
        //        Select = _userConfiguration.KeySelect,
        //        Pause = _userConfiguration.KeyPause,

        //        MouseId = _userConfiguration.MouseId,
        //        Interact = _userConfiguration.MouseInteract
        //    };
        //}
    }
}

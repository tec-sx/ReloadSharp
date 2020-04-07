namespace Core.Config
{
    using System.Drawing;
    using Silk.NET.Windowing.Common;
    
    public static class ConfigurationFactory
    {
        public static WindowOptions CreateWindowOptions()
        {
            var display = Configuration.Settings.Display;
            var info = Configuration.Settings.Info;

            var options = display.UseVulkan ? WindowOptions.DefaultVulkan : WindowOptions.Default;
            
            options.Title = $"{info.ProgramName} - v.{info.ProgramVersion}";
            options.Size = new Size(display.Width, display.Height);
            options.WindowState = display.IsFullScreen ? WindowState.Fullscreen : WindowState.Normal;
            options.UpdatesPerSecond = display.TargetFps;
            options.FramesPerSecond = display.TargetFps;
            options.VSync = display.VSync ? VSyncMode.On : VSyncMode.Adaptive;
            options.RunningSlowTolerance = 5;

            return options;
        }
    }
}
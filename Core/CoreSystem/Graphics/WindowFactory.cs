namespace Core.CoreSystem.Graphics
{
    using Config;
    using Logging.Exceptions;
    using Silk.NET.Windowing;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    internal static class WindowFactory
    {
        public static IWindow CreateWindow()
        {
            var windowOptions = CreateWindowOptionsFromSettings();

            if (Configuration.Settings.Display.UseVulkan)
            {
                if (TryCreateVulkanWindow(windowOptions, out var vulkanWindow))
                {
                    return vulkanWindow;
                }

                GraphicsBackendNotSupported.Warning("Vulkan is not supported, fallback to OpenGl.");

                Configuration.Settings.Display.UseVulkan = false;
                Configuration.SaveSettings();
            }

            if(TryCreateOpenGlWindow(windowOptions, out var openGlWindow))
            {
                return openGlWindow;
            }

            throw GraphicsBackendNotSupported.Exception("Open GL is not supported.");
        }

        private static WindowOptions CreateWindowOptionsFromSettings()
        {
            var info = Configuration.Settings.Info;
            var display = Configuration.Settings.Display;

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

        private static unsafe bool TryCreateVulkanWindow(WindowOptions options, out IWindow window)
        {
            window = Window.Create(options) as IVulkanWindow;

            if (window is null || !window.IsVulkanSupported)
            {
                return false;
            }

            window.Initialize();

            return true;
        }

        private static bool TryCreateOpenGlWindow(WindowOptions options, out IWindow window)
        {
            window = Window.Create(options);

            if (window is null)
            {
                return false;
            }

            window.Initialize();

            return true;
        }
    }
}
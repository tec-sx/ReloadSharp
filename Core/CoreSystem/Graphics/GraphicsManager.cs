namespace Core.CoreSystem.Graphics
{
    using System.Drawing;
    using Config;
    using Device;
    using ErrorHandling;
    using Device.OpenGl;
    using Core.CoreSystem.Graphics.Device.Vulkan;
    using Silk.NET.Windowing.Common;

    using SilkWindow = Silk.NET.Windowing.Window;
    
    internal sealed class GraphicsManager : IGraphicsManager
    {
        public IWindow Window { get; private set; }
        public IGraphicsDevice Device { get; private set; }


        public void DisposeResources()
        {
            Window.Dispose();
            Device.Dispose();
        }

        public void CreateWindow()
        {
            var windowOptions = CreateWindowOptionsFromSettings();

            if (Configuration.Settings.Display.UseVulkan)
            {
                if (TryCreateVulkanWindow(windowOptions, out var vulkanWindow))
                {
                    Window = vulkanWindow;
                    return;
                }

                GraphicsBackendNotSupported.Warning("Vulkan is not supported, fallback to OpenGl.");

                Configuration.Settings.Display.UseVulkan = false;
                Configuration.SaveSettings();
            }

            if(TryCreateOpenGlWindow(windowOptions, out var openGlWindow))
            {
                Window = openGlWindow;
                return;
            }

            throw GraphicsBackendNotSupported.Exception("Open GL is not supported.");
        }

        public void CreateDevice()
        {
            if (Configuration.Settings.Display.UseVulkan)
            {
                Device = new VulkanDevice();
            }
            else
            {
                Device = new OpenGlDevice();
            }
            
            Device.Initialize(Window);
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

        private static bool TryCreateVulkanWindow(WindowOptions options, out IWindow window)
        {
            window = SilkWindow.Create(options) as IVulkanWindow;

            if (window is null || !window.IsVulkanSupported)
            {
                return false;
            }

            window.Initialize();

            return true;
        }

        private static bool TryCreateOpenGlWindow(WindowOptions options, out IWindow window)
        {
            window = SilkWindow.Create(options);

            if (window is null)
            {
                return false;
            }

            window.Initialize();

            return true;
        }
    }
}
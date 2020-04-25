namespace Engine.Graphics
{
    using System.Drawing;
    using Device;
    using Device.OpenGl;
    using Engine.Graphics.Device.Vulkan;
    using Silk.NET.Windowing.Common;

    using SilkWindow = Silk.NET.Windowing.Window;
    using System;

    public sealed class GraphicsManager
    {
        private DisplayConfiguration _displayConfiguration;

        public IWindow Window { get; private set; }
        public IGraphicsDevice Device { get; private set; }

        public void DisposeResources()
        {
            Window.Dispose();
            Device.Dispose();
        }

        public void Initialize(DisplayConfiguration displayConfiguration)
        {
            _displayConfiguration = displayConfiguration;
        }

        public void CreateWindow()
        {
            var windowOptions = CreateWindowOptionsFromSettings();

            if (_displayConfiguration.EnableVulkan)
            {
                if (TryCreateVulkanWindow(windowOptions, out var vulkanWindow))
                {
                    Window = vulkanWindow;
                    return;
                }

                throw new NotSupportedException("Vulkan is not supported.");
            }

            if (TryCreateOpenGlWindow(windowOptions, out var openGlWindow))
            {
                Window = openGlWindow;
                return;
            }

            throw new NotSupportedException("Open GL is not supported.");
        }

        public void CreateDevice()
        {
            if (_displayConfiguration.EnableVulkan)
            {
                Device = new VulkanDevice();
            }
            else
            {
                Device = new OpenGlDevice();
            }

            Device.Initialize(Window);
        }

        private WindowOptions CreateWindowOptionsFromSettings()
        {
            var options = _displayConfiguration.EnableVulkan ? WindowOptions.DefaultVulkan : WindowOptions.Default;

            options.Title = _displayConfiguration.WindowTitle;
            options.Size = new Size(_displayConfiguration.Resolution.X, _displayConfiguration.Resolution.Y);
            options.WindowState = _displayConfiguration.InFullScreen ? WindowState.Fullscreen : WindowState.Normal;
            options.UpdatesPerSecond = _displayConfiguration.TargetFps;
            options.FramesPerSecond = _displayConfiguration.TargetFps;
            options.VSync = _displayConfiguration.EnableVSync ? VSyncMode.On : VSyncMode.Adaptive;
            options.RunningSlowTolerance = 5;
            options.UseSingleThreadedWindow = false;

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
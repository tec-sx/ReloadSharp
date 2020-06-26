namespace Reload.Graphics
{
    using Silk.NET.Windowing.Common;
    using System;
    using System.Drawing;
    using Silk.NET.Windowing;

    /// <summary>
    /// The main graphics manager class. Add as singleton in the
    /// service manager.
    /// </summary>
    public sealed class GraphicsManager
    {
        /// <summary>
        /// Creates a new Silk.NET window with the provided configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="IWindow"></returns>
        /// <exception cref="NotSupportedException"></exception>
        public unsafe IWindow CreateWindow(DisplayConfiguration displayConfiguration)
        {
            var options = CreateWindowOptionsFromConfiguration(displayConfiguration);

            return Window.Create(options);
        }

        /// <summary>
        /// Creates WindowOptions used by Silk.NET Window.Create static method
        ///  from a user defined display configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="WindowOptions"></returns>
        private static WindowOptions CreateWindowOptionsFromConfiguration(DisplayConfiguration displayConfiguration)
        {
            var options = displayConfiguration.EnableVulkan ? WindowOptions.DefaultVulkan : WindowOptions.Default;

            options.Title = displayConfiguration.WindowTitle;
            options.Size = new Size(displayConfiguration.Resolution.X, displayConfiguration.Resolution.Y);
            options.WindowState = displayConfiguration.InFullScreen ? WindowState.Fullscreen : WindowState.Normal;
            options.UpdatesPerSecond = displayConfiguration.TargetFps;
            options.FramesPerSecond = displayConfiguration.TargetFps;
            options.VSync = displayConfiguration.EnableVSync ? VSyncMode.On : VSyncMode.Off;
            options.RunningSlowTolerance = 5;
            options.UseSingleThreadedWindow = false;
            options.IsEventDriven = false;
            options.WindowBorder = displayConfiguration.WindowBorder;
            options.ShouldSwapAutomatically = false;
            options.Position = displayConfiguration.Position;

            return options;
        }
    }
}
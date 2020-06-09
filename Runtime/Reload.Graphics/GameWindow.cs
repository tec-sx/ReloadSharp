namespace Reload.Graphics
{
    using Reload.Graphics.Contexts;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class GameWindow
    {
        public static IWindow Handle { get; private set; }
        public static IGraphicsContext Context { get; private set; }

        public static void Create(DisplayConfiguration configuration)
        {
            var options = CreateWindowOptionsFromConfiguration(configuration);

            Handle = Silk.NET.Windowing.Window.Create(options);
            Context = GraphicsContextFactory.CreateContext(Handle);
        }

        /// <summary>
        /// Creates WindowOptions used by Silk.NET Window.Create static method
        ///  from a user defined display configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="WindowOptions"></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            options.WindowBorder = WindowBorder.Fixed;
            options.ShouldSwapAutomatically = false;

            return options;
        }
    }
}

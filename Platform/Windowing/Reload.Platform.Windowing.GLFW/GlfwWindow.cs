using Reload.Core.Graphics;
using Reload.Core.Windowing;
using Silk.NET.Windowing.Common;
using System.Drawing;
using Reload.Platform.Windowing.GLFW.Extensions;
using Silk.NET.Windowing;

namespace Reload.Platform.Windowing.GLFW
{
    /// <summary>
    /// The Glfw window implementation.
    /// </summary>
    public class GlfwWindow : ProgramWindow
    {
        private readonly IWindow _nativeWindow;

        private WindowOptions _windowOptions;

        private bool _disposed;

        /// <summary>
        /// Prevents a default instance of the <see cref="GlfwWindow"/> class from being created.
        /// </summary>
        private GlfwWindow()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlfwWindow"/> class.
        /// </summary>
        /// <param name="displayConfiguration">The display configuration.</param>
        public GlfwWindow(DisplayConfiguration displayConfiguration)
        {
            _windowOptions = CreateWindowOptionsFromConfiguration(displayConfiguration);
            _nativeWindow = Window.Create(_windowOptions);

            GetProcAddress = _nativeWindow.GLContext.GetProcAddress;        
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            _nativeWindow.Load += Load;
            _nativeWindow.Update += Update;
            _nativeWindow.Render += Render;
            _nativeWindow.Move += Move;
            _nativeWindow.Resize += Resize;
            _nativeWindow.FocusChanged += FocusChanged;
            _nativeWindow.Closing += Closing;
        }

        /// <inheritdoc/>
        public override void ShutDown()
        {
            _nativeWindow.Close();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _nativeWindow.Dispose();
            }

            _disposed = true;
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
            options.UseSingleThreadedWindow = true;
            options.IsEventDriven = false;
            options.WindowBorder = displayConfiguration.WindowBorder.ToSilkNetWindowBorder();
            options.ShouldSwapAutomatically = false;
            options.Position = displayConfiguration.Position;

            return options;
        }
    }
}

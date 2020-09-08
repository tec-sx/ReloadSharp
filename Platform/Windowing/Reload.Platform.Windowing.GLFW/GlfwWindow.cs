using Reload.Core.Windowing;
using Silk.NET.Windowing.Common;
using System.Drawing;
using Reload.Platform.Windowing.GLFW.Extensions;
using Silk.NET.Windowing;
using System;
using Reload.Core.Configuration;

namespace Reload.Platform.Windowing.GLFW
{
    /// <summary>
    /// The Glfw window implementation.
    /// </summary>
    public class GlfwWindow : IProgramWindow
    {
        private IWindow _nativeWindow;

        private bool _disposed;

        /// <inheritdoc/>
        public WindowingAPIType Api => WindowingAPIType.Glfw;

        /// <inheritdoc/>
        public Size Size => _nativeWindow.Size;

        /// <inheritdoc/>
        public Point Position => _nativeWindow.Position;

        /// <inheritdoc/>
        public bool IsFullScreen { get; set; }

        /// <inheritdoc/>
        public bool IsVsyncOn { get; set; }

        /// <inheritdoc/>
        public Func<string, IntPtr> GetProcAddress { get; init; }
        
        /// <inheritdoc/>
        public Action Load { get; set; }

        /// <inheritdoc/>
        public Action<double> Update { get; set; }

        /// <inheritdoc/>
        public Action<double> Render { get; set; }

        /// <inheritdoc/>
        public Action<Point> Move { get; set; }

        /// <inheritdoc/>
        public Action<Size> Resize { get; set; }

        /// <inheritdoc/>
        public Action<bool> FocusChanged { get; set; }

        /// <inheritdoc/>
        public Action Closing { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlfwWindow"/> class.
        /// </summary>
        public GlfwWindow(SystemConfiguration configuration)
        {
            WindowOptions options = CreateWindowOptionsFromConfiguration(configuration.Display);

            _nativeWindow = Window.Create(options);

            GetProcAddress = _nativeWindow.GLContext.GetProcAddress;
        }

        /// <inheritdoc/>
        public void StartUp()
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
        public void ShutDown()
        {
            _nativeWindow.Close();
        }

        /// <summary>
        /// Creates WindowOptions used by Silk.NET Window.Create static method
        ///  from a user defined display configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="WindowOptions"></returns>
        private static WindowOptions CreateWindowOptionsFromConfiguration(DisplayConfiguration displayConfiguration)
        {
            var options = WindowOptions.Default;

            options.Title = displayConfiguration.WindowTitle;
            options.Size = new Size(displayConfiguration.Resolution.Width, displayConfiguration.Resolution.Height);
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

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
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

    }
}

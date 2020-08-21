using Reload.Core.Game;
using System;
using System.Drawing;

namespace Reload.Core.Graphics
{
    /// <summary>
    /// The game window base.
    /// </summary>
    public abstract class ProgramWindow : ICoreSystem, IDisposable
    {
        /// <summary>
        /// Gets the windowing backend type.
        /// </summary>
        public WindowingAPIType BackendType { get; init; }

        /// <summary>
        /// Gets or sets the window width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the window height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the window X position.
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the window Y position.
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the window full is screen.
        /// </summary>
        public bool IsFullScreen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether vsync is on.
        /// </summary>
        public bool IsVsyncOn { get; set; }

        /// <summary>
        /// Gets the native window handle.
        /// </summary>
        public Func<string, IntPtr> GetProcAddress { get; protected init; }


        /// <summary>
        /// Executes on window startup.
        /// </summary>
        public Action Load { get; protected set; }

        /// <summary>
        /// Executes on window update.
        /// </summary>
        public Action<double> Update { get; protected set; }

        /// <summary>
        /// Executes on window render.
        /// </summary>
        public Action<double> Render { get; protected set; }

        /// <summary>
        /// Executed when the window is moved.
        /// </summary>
        public Action<Point> Move { get; protected set; }

        /// <summary>
        /// Executed when the window is resized.
        /// </summary>
        public Action<Size> Resize { get; protected set; }

        /// <summary>
        /// Executed when the window focus changes.
        /// </summary>
        public Action<bool> FocusChanged { get; protected set; }

        /// <summary>
        /// Executes on window closing.
        /// </summary>
        public Action Closing { get; protected set; }

        /// <inheritdoc/>
        public abstract void Initialize();

        /// <inheritdoc/>
        public abstract void ShutDown();

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
        protected abstract void Dispose(bool disposing);
    }
}

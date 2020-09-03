namespace Reload.Graphics
{
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    public record DisplayConfiguration
    {
        /// <summary>
        /// User set window/screen resolution.
        /// </summary>
        public Size Resolution { get; init; }

        /// <summary>
        /// Window position.
        /// </summary>
        public Point Position { get; init; }

        /// <summary>
        /// User set screen refresh rate.
        /// </summary>
        public int RefreshRate { get; init; }

        /// <summary>
        /// System set maximum frames per second.
        /// </summary>
        public int TargetFps { get; init; }

        /// <summary>
        /// User set full screen or windowed mode.
        /// </summary>
        public bool InFullScreen { get; init; }

        /// <summary>
        /// User set VSync enable.
        /// </summary>
        public bool EnableVSync { get; init; }

        /// <summary>
        /// System set window title.
        /// </summary>
        public string WindowTitle { get; init; }

        /// <summary>
        /// Hide window borders.
        /// </summary>
        public WindowBorder WindowBorder { get; init; }


    }
}

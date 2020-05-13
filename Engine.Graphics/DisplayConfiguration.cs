namespace Engine.Graphics
{
    using System.Drawing;

    public struct DisplayConfiguration
    {
        /// <summary>
        /// User set window/screen resolution.
        /// </summary>
        public Point Resolution { get; set; }

        /// <summary>
        /// User set screen refresh rate.
        /// </summary>
        public int RefreshRate { get; set; }

        /// <summary>
        /// System set maximum frames per second.
        /// </summary>
        public int TargetFps { get; set; }

        /// <summary>
        /// User set full screen or windowed mode.
        /// </summary>
        public bool InFullScreen { get; set; }

        /// <summary>
        /// User set VSync enable.
        /// </summary>
        public bool EnableVSync { get; set; }

        /// <summary>
        /// User set enable Vulkan backend.
        /// </summary>
        public bool EnableVulkan { get; set; }

        /// <summary>
        /// System set window title.
        /// </summary>
        public string WindowTitle { get; set; }
    }
}

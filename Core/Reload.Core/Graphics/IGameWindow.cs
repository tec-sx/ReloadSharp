using Reload.Core.Game;

namespace Reload.Core.Graphics
{
    public interface IGameWindow : ISubSystem
    {
        /// <summary>
        /// Gets or sets the window width.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the window height.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the window X position.
        /// </summary>
        int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the window Y position.
        /// </summary>
        int PositionY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the window full is screen.
        /// </summary>
        bool IsFullScreen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether vsync is on.
        /// </summary>
        bool IsVsyncOn { get; set; }

        /// <summary>
        /// Executes on window startup.
        /// </summary>
        void OnStarting();

        /// <summary>
        /// Executes on window update.
        /// </summary>
        void OnUpdate(double deltaTime);

        /// <summary>
        /// Executes on window render.
        /// </summary>
        void OnRender(double deltaTime);

        /// <summary>
        /// Executes on window close.
        /// </summary>
        void OnClose();
    }
}

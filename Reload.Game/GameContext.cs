namespace Reload.Game
{
    using Reload.Game.Graphics;

    /// <summary>
    /// Generic version of <see cref="GameContext"/>. The later is used to describe a generic game Context.
    /// This version enables us to constraint the game context to a specifc toolkit and ensures a better cohesion
    /// between the various toolkit specific classes, such as InputManager, GameWindow.
    /// </summary>
    /// <typeparam name="THandle"></typeparam>
    public abstract class GameContext<THandle>
    {
        /// <summary>
        /// Underlying control associated with context.
        /// </summary>
        public THandle Handle { get; internal set; }

        /// <summary>
        /// The requested width.
        /// </summary>
        internal int RequestedWidth;

        /// <summary>
        /// The requested height.
        /// </summary>
        internal int RequestedHeight;

        internal PixelFormat RequestedBackBufferFormat;

        /// <summary>
        /// The requested depth stencil format.
        /// </summary>
        internal PixelFormat RequestedDepthStncilFormat;

        /// <summary>
        /// Indicate whether the game must initialize the default database when it starts running.
        /// </summary>
        public bool InitializeDatabase { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameContext" /> class.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="requestedWidth">Width of the requested.</param>
        /// <param name="requestedHeight">Height of the requested.</param>
        protected GameContext(THandle control, int requestedWidth = 0, int requestedHeight = 0)
        {
            Handle = control;
            RequestedWidth = requestedWidth;
            RequestedHeight = requestedHeight;
        }
    }
}

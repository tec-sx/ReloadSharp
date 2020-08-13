using System;

namespace Reload.Core.Game
{
    /// <summary>
    /// The base game system class.
    /// Can be instantiated through <see cref="GameBuilder"/>
    /// </summary>
    public abstract class GameSystem : ISubSystem
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the sub systems provider.
        /// </summary>
        public IServiceProvider SubSystems { get; init; }

        /// <summary>
        /// Static event that will be fired when a game is initialized
        /// </summary>
        public static event Action GameStarted;

        /// <summary>
        /// Static event that will be fired when a game is destroyed
        /// </summary>
        public static event Action GameDestroyed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSystem"/> class.
        /// </summary>
        public GameSystem()
        { }

        /// <inheritdoc/>
        public abstract void Initialize();

        /// <inheritdoc/>
        public abstract void ShutDown();

        /// <summary>
        /// Begins the game loop.
        /// </summary>
        public abstract void Run();
    }
}

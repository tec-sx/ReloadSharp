using DryIoc;
using System;

namespace Reload.Core.Game
{
    /// <summary>
    /// The base game system class.
    /// Can be instantiated through <see cref="GameBuilder"/>
    /// </summary>
    public abstract class GameSystem : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the sub systems provider.
        /// </summary>
        internal IContainer SubSystems { get; init; }

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
        {
            SubSystems.RegisterInitializer<ISubSystem>((subSystem, resolver) => subSystem.Initialize());
            SubSystems.RegisterDisposer<ISubSystem>(subSystem => subSystem.ShutDown());
        }

        /// <summary>
        /// Itterates throug all included sub-systems and calls the <see cref="ISubSystem.Initialize"/>
        /// for each of them. afterwards it calls the <see cref="OnInitialize"/> method.
        /// </summary>
        public void Initilize()
        { 
            OnInitialize();
        }

        /// <summary>
        /// Shuts down the game system.
        /// </summary>
        public void ShutDown()
        {
            OnShutDown();
        }

        /// <summary>
        /// Executes upon initalization of the game system.
        /// Should contain all logic used to initialize parts of the system
        /// other than the <see cref="SubSystems"/> that need manual
        /// intialization.
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// Executes upon shutting down the game system.
        /// Should contain all logic used to clean up resources
        /// other than the <see cref="SubSystems"/> that need manual
        /// clean up.
        /// </summary>
        protected abstract void OnShutDown();

        /// <summary>
        /// Begins the game loop.
        /// </summary>
        public abstract void Run();

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
            if (!_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                SubSystems.Dispose();
            }

            _isDisposed = true;
        }
    }
}

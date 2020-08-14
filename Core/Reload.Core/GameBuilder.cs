using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reload.Core.Audio;
using Reload.Core.Extensions;
using Reload.Core.Game;
using Reload.Core.Graphics;
using Reload.Core.Properties;
using Reload.Core.Utilities;

namespace Reload.Core
{
    /// <summary>
    /// The game builder is used to create instance from
    /// the game system with all subsystems.
    /// </summary>
    public class GameBuilder<T> where T : GameSystem, new()
    {

        private readonly IServiceCollection _subSystemsCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBuilder"/> class.
        /// </summary>
        public GameBuilder()
        {
            _subSystemsCollection = new ServiceCollection();
            Logger.Log().Information(Resources.BuildStartingMessage);
        }

        /// <summary>
        /// Builds the game.
        /// </summary>
        /// <returns>A T.</returns>
        public T Build()
        {
            return new T()
            {
                SubSystems = _subSystemsCollection.BuildServiceProvider()
            };
        }

        /// <summary>
        /// Adds the window sub-system.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<T> WithWindow(IGameWindow window)
        {
            _subSystemsCollection.TryAddSingleton(window);
            Logger.Log().Information(Resources.WithWindowMessage ,window?.BackendType.GetDescription());

            return this;
        }

        /// <summary>
        /// Adds the graphics backend sub-system.
        /// </summary>
        /// <param name="graphicsBackend">The graphics backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<T> WithGraphicsBackend(IGraphicsBackend graphicsBackend)
        {
            _subSystemsCollection.TryAddSingleton(graphicsBackend);
            Logger.Log().Information(Resources.WithGraphicsBackendMessage, graphicsBackend?.Type.GetDescription());

            return this;
        }

        /// <summary>
        /// Adds the audio backend sub-system.
        /// </summary>
        /// <param name="audioBackend">The audio backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<T> WithAudioBackend(IAudioBackend audioBackend)
        {
            _subSystemsCollection.TryAddSingleton(audioBackend);
            Logger.Log().Information(Resources.WithAudioBackendMessage ,audioBackend?.Type.GetDescription());

            return this;
        }
    }
}

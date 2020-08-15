using DryIoc;
using Reload.Core.Audio;
using Reload.Core.Exceptions;
using Reload.Core.Extensions;
using Reload.Core.Game;
using Reload.Core.Graphics;
using Reload.Core.Input;
using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core
{
    /// <summary>
    /// The game builder is used to create instance from
    /// the game system with all subsystems.
    /// </summary>
    public sealed class GameBuilder<TGame> : IDisposable 
        where TGame : GameSystem, new()
    {
        private readonly PlatformOS _platform;

        private readonly IContainer _subSystems;

        /// <summary>
        /// Prevents a default instance of the <see cref="GameBuilder"/> class 
        /// without specifying the OS platform from being created.
        /// </summary>
        private GameBuilder()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBuilder"/> class.
        /// </summary>
        public GameBuilder(PlatformOS platform)
        {
            _platform = platform;
            _subSystems = new Container();

            Logger.Log().Information(Resources.BuildStartingMessage);
        }

        /// <summary>
        /// Builds the game.
        /// </summary>
        /// <returns>A T.</returns>
        public TGame BuildForPlatform()
        {
            return new TGame()
            {
                SubSystems = _subSystems
            };
        }

        /// <summary>
        /// Adds the window sub-system.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithWindow<T>() where T : class, IGameWindow
        {
            if (!_platform.CheckWindowCompatability<T>())
            {
                throw new ReloadWindowBackendNotSupportedException();
            }

            _subSystems.Register<IGameWindow, T>(Reuse.Singleton);
            _subSystems.RegisterInitializer<IGameWindow>((window, resolver) => 
                Logger.Log().Information(Resources.WithWindowMessage, window?.BackendType.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds the graphics backend sub-system.
        /// </summary>
        /// <param name="graphicsBackend">The graphics backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithGraphicsBackend<T>() where T : class, IGraphicsBackend
        {
            if (!_platform.CheckGraphicsBackendCompatability<T>())
            {
                throw new ReloadGraphicsBackendNotSupportedException();
            }

            _subSystems.Register<IGraphicsBackend, T>(Reuse.Singleton);
            _subSystems.RegisterInitializer<IGraphicsBackend>((graphicsBackend, resolver) =>
                Logger.Log().Information(Resources.WithGraphicsBackendMessage, graphicsBackend?.Type.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds the audio backend sub-system.
        /// </summary>
        /// <param name="audioBackend">The audio backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithAudioBackend<T>() where T : class, IAudioBackend
        {
            if (!_platform.CheckAudioBackendCompatability<T>())
            {
                throw new ReloadAudioBackendNotSupportedException();
            }

            _subSystems.Register<IAudioBackend, T>(Reuse.Singleton);
            _subSystems.RegisterInitializer<IAudioBackend>((audioBackend, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage ,audioBackend?.Type.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds an input sub-system.
        /// </summary>
        /// <param name="audioBackend">The audio backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithInput<T>() where T : class, IInputSystem
        {
            if (!_platform.CheckInputCompatability<T>())
            {
                throw new ReloadInputNotSupportedException();
            }

            _subSystems.Register<IInputSystem, T>(Reuse.Singleton);
            _subSystems.RegisterInitializer<IInputSystem>((inputSystem, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage, inputSystem?.Source.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds a sub-system.
        /// </summary>
        /// <param name="audioBackend">The audio backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithSubSystem<T>(SubSystemLifetime lifetime) where T : class, ISubSystem
        {
            IReuse reuse = lifetime switch
            {
                SubSystemLifetime.Singleton => Reuse.Singleton,
                SubSystemLifetime.Transient => Reuse.Transient,
                SubSystemLifetime.Scoped => Reuse.Scoped,
                _ => null
            };

            if (reuse != null)
            {
                _subSystems.Register<ISubSystem, T>(reuse);
                _subSystems.RegisterInitializer<ISubSystem>((subSystem, resolver) =>
                    Logger.Log().Information(Resources.WithAudioBackendMessage, subSystem.ToString()));
            }

            return this;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _subSystems.Dispose();
        }
    }
}

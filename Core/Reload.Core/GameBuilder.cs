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

        private readonly string _name;

        #region Core Systems

        private ProgramWindow _window;

        private GraphicsAPI _graphics;

        private AudioAPI _audio;
        
        private IInputSystem _input;

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="GameBuilder"/> class 
        /// without specifying the OS platform from being created.
        /// </summary>
        private GameBuilder()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBuilder"/> class.
        /// </summary>
        public GameBuilder(string name, PlatformOS platform)
        {
            _name = name;
            _platform = platform;
            Logger.Log().Information(Resources.BuildStartingMessage);
        }

        /// <summary>
        /// Builds the game.
        /// </summary>
        /// <returns>A T.</returns>
        public TGame BuildForPlatform()
        {
            if (_window == null) throw new ReloadArgumentNullException(typeof(ProgramWindow).ToString());

            if (_graphics == null) throw new ReloadArgumentNullException(typeof(GraphicsAPI).ToString());

            if (_input == null) throw new ReloadArgumentNullException(typeof(IInputSystem).ToString());

            if (_audio == null) _audio = new NullAudioAPI();

            return new TGame()
            {
                Name = _name,
                Window = _window,
                Graphics = _graphics,
                Input = _input,
                Audio = _audio
            };
        }

        /// <summary>
        /// Adds the window sub-system.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithWindow<T>() where T : ProgramWindow, new()
        {
            if (!_platform.CheckWindowCompatability<T>())
            {
                throw new ReloadWindowBackendNotSupportedException();
            }

            _window = new T();

            return this;
        }

        /// <summary>
        /// Adds the graphics backend sub-system.
        /// </summary>
        /// <param name="graphicsBackend">The graphics backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithGraphicsAPI<T>() where T : GraphicsAPI
        {
            if (!_platform.CheckGraphicsBackendCompatability<T>())
            {
                throw new ReloadGraphicsBackendNotSupportedException();
            }

            _coreSystems.Register<GraphicsAPI, T>(Reuse.Singleton);
            _coreSystems.RegisterInitializer<GraphicsAPI>((graphicsBackend, resolver) =>
                Logger.Log().Information(Resources.WithGraphicsBackendMessage, graphicsBackend?.Type.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds the audio backend sub-system.
        /// </summary>
        /// <param name="audioBackend">The audio backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithAudioAPI<T>() where T : AudioAPI
        {
            if (!_platform.CheckAudioBackendCompatability<T>())
            {
                throw new ReloadAudioBackendNotSupportedException();
            }

            _coreSystems.Register<AudioAPI, T>(Reuse.Singleton);
            _coreSystems.RegisterInitializer<AudioAPI>((audioBackend, resolver) =>
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

            _coreSystems.Register<IInputSystem, T>(Reuse.Singleton);
            _coreSystems.RegisterInitializer<IInputSystem>((inputSystem, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage, inputSystem?.Source.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds a sub-system.
        /// </summary>
        /// <param name="audioBackend">The audio backend.</param>
        /// <returns>A GameBuilder.</returns>
        public GameBuilder<TGame> WithSubSystem<T>(Lifetime lifetime) where T : class, ICoreSystem
        {
            IReuse reuse = lifetime switch
            {
                Lifetime.Singleton => Reuse.Singleton,
                Lifetime.Transient => Reuse.Transient,
                Lifetime.Scoped => Reuse.Scoped,
                _ => throw new ReloadInvalidEnumArgumentException()
            };

            _coreSystems.Register<ICoreSystem, T>(reuse);
            _coreSystems.RegisterInitializer<ICoreSystem>((subSystem, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage, subSystem.ToString()));

            return this;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _coreSystems.Dispose();
        }
    }
}

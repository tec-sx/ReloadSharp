#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
using DryIoc;
using Reload.Core.Audio;
using Reload.Core.Configuration;
using Reload.Core.Exceptions;
using Reload.Core.Extensions;
using Reload.Core.Game;
using Reload.Core.Graphics;
using Reload.Core.Input;
using Reload.Core.Properties;
using Reload.Core.Utilities;
using Reload.Core.Windowing;
using System;

namespace Reload.Core
{
    /// <summary>
    /// The game builder is used to create instance from
    /// the game system with all subsystems.
    /// </summary>
    public sealed class ProgramBuilder<TGame> : IDisposable
        where TGame : GameSystem, new()
    {
        private readonly PlatformOS _platform;

        private readonly string _name;

        private readonly Container _services;

        #region Core Systems

        private IProgramWindow _window;

        private GraphicsAPI _graphics;

        private AudioAPI _audio;
        
        private IInputSystem _input;

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="GameBuilder"/> class 
        /// without specifying the OS platform from being created.
        /// </summary>
        private ProgramBuilder()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBuilder"/> class.
        /// </summary>
        public ProgramBuilder(string name, PlatformOS platform)
        {
            _name = name;
            _platform = platform;
            Logger.Log().Information(Resources.BuildStartingMessage);
        }

        /// <summary>
        /// Builds the game.
        /// </summary>
        /// <returns>A T.</returns>
        public TGame BuildForPlatform(SystemConfiguration configuration)
        {
            if (_window == null) throw new ReloadArgumentNullException(typeof(IProgramWindow).ToString());

            if (_graphics == null) throw new ReloadArgumentNullException(typeof(GraphicsAPI).ToString());

            if (_input == null) throw new ReloadArgumentNullException(typeof(IInputSystem).ToString());

            if (_audio == null) _audio = new NullAudioAPI();

            TGame game =  new TGame()
            {
                Name = _name,
                Window = _window,
                Graphics = _graphics,
                Input = _input,
                Audio = _audio
            };

            game.Configure(configuration);

            return game;
        }

        /// <summary>
        /// Adds the window sub-system.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>A GameBuilder.</returns>
        public ProgramBuilder<TGame> WithWindow<T>(DisplayConfiguration configuration) where T : IProgramWindow, new()
        {
            if (!_platform.CheckWindowCompatability<T>())
            {
                throw new ReloadWindowBackendNotSupportedException();
            }

            _window = new T();

            Logger.Log().Information(Resources.WithWindowMessage, _window.ToString());

            return this;
        }

        /// <summary>
        /// Adds the graphics backend sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public ProgramBuilder<TGame> WithGraphicsAPI<T>() where T : GraphicsAPI, new()
        {
            if (!_platform.CheckGraphicsBackendCompatability<T>())
            {
                throw new ReloadGraphicsBackendNotSupportedException();
            }

            _graphics = new T();
            
            Logger.Log().Information(Resources.WithGraphicsBackendMessage, _graphics.ToString());

            return this;
        }

        /// <summary>
        /// Adds the audio backend sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public ProgramBuilder<TGame> WithAudioAPI<T>() where T : AudioAPI, new()
        {
            if (!_platform.CheckAudioBackendCompatability<T>())
            {
                throw new ReloadAudioBackendNotSupportedException();
            }

            _audio = new T();

            Logger.Log().Information(Resources.WithAudioBackendMessage, _audio.To);

            return this;
        }

        /// <summary>
        /// Adds an input sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public ProgramBuilder<TGame> WithInput<T>() where T : class, IInputSystem
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
        public ProgramBuilder<TGame> WithSubSystem<T>(Lifetime lifetime) where T : class, ISubSystem
        {
            IReuse reuse = lifetime switch
            {
                Lifetime.Singleton => Reuse.Singleton,
                Lifetime.Transient => Reuse.Transient,
                Lifetime.Scoped => Reuse.Scoped,
                _ => throw new ReloadInvalidEnumArgumentException()
            };

            _coreSystems.Register<ISubSystem, T>(reuse);
            _coreSystems.RegisterInitializer<ISubSystem>((subSystem, resolver) =>
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

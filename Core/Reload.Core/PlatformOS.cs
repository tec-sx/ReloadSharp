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
    /// The OS platform base class. Every opering system implementation
    /// must inherit from this class.
    /// </summary>
    public abstract class PlatformOS : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// Gets the sub systems container.
        /// </summary>
        protected IContainer SystemsContainer { get; }

        /// <summary>
        /// Indicates whether the program has been build successfully for the platform.
        /// </summary>
        protected bool IsSuccessfullyBuilt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformOS"/> class.
        /// </summary>
        public PlatformOS()
        {
            SystemsContainer = new Container();
        }

        /// <summary>
        /// Registers the main program.
        /// </summary>
        public PlatformOS RegisterMainProgram<TProgram>() where TProgram : GameSystem
        {
            SystemsContainer.Register<GameSystem, TProgram>();
            SystemsContainer.RegisterInitializer<GameSystem>((program, resolver) =>
                Logger.Log().Information(Resources.BuildStartingMessage, program.Name));

            return this;
        }

        /// <summary>
        /// Adds a configuration.
        /// </summary>
        /// <returns>A PlatformOS.</returns>
        public PlatformOS WithConfiguration(SystemConfiguration configuration)
        {
            SystemsContainer.RegisterInstance(configuration);
            SystemsContainer.RegisterInitializer<SystemConfiguration>((configuration, resolver) =>
                Logger.Log().Information(Resources.WithConfiguration, configuration.ToString()));

            return this;
        }

        /// <summary>
        /// Adds the window sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public PlatformOS WithWindow<TWindow>() where TWindow : class, IProgramWindow, ISubSystem, IDisposable
        {
            if (!CheckWindowCompatability<TWindow>())
            {
                throw new ReloadWindowBackendNotSupportedException();
            }

            SystemsContainer.Register<IProgramWindow, TWindow>();
            SystemsContainer.RegisterInitializer<IProgramWindow>((window, resolver) =>
                Logger.Log().Information(Resources.WithWindowMessage, window.Api.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds the graphics backend sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public PlatformOS WithGraphicsAPI<TGraphics>() where TGraphics : GraphicsAPI, ISubSystem, IDisposable
        {
            if (!CheckGraphicsBackendCompatability<TGraphics>())
            {
                throw new ReloadGraphicsBackendNotSupportedException();
            }

            SystemsContainer.Register<GraphicsAPI, TGraphics>();
            SystemsContainer.RegisterInitializer<GraphicsAPI>((graphicsApi, resolver) =>
                Logger.Log().Information(Resources.WithGraphicsBackendMessage, graphicsApi.Type.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds the audio backend sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public PlatformOS WithAudioAPI<TAudio>() where TAudio : AudioAPI, ISubSystem, IDisposable
        {
            if (!CheckAudioBackendCompatability<TAudio>())
            {
                throw new ReloadAudioBackendNotSupportedException();
            }

            SystemsContainer.Register<AudioAPI, TAudio>();
            SystemsContainer.RegisterInitializer<AudioAPI>((audioApi, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage, audioApi.Type.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds an input sub-system.
        /// </summary>
        /// <returns>A GameBuilder.</returns>
        public PlatformOS WithInput<TInput>() where TInput : class, IInputSystem, ISubSystem, IDisposable
        {
            if (!CheckInputCompatability<TInput>())
            {
                throw new ReloadInputNotSupportedException();
            }

            SystemsContainer.Register<IInputSystem, TInput>(Reuse.Singleton);
            SystemsContainer.RegisterInitializer<IInputSystem>((inputSystem, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage, inputSystem?.Source.GetDescription()));

            return this;
        }

        /// <summary>
        /// Adds a new sub system.
        /// </summary>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>A PlatformOS.</returns>
        public PlatformOS WithSubSystem<T>(Lifetime lifetime) where T : class, ISubSystem
        {
            IReuse reuse = lifetime switch
            {
                Lifetime.Singleton => Reuse.Singleton,
                Lifetime.Transient => Reuse.Transient,
                Lifetime.Scoped => Reuse.Scoped,
                _ => throw new ReloadInvalidEnumArgumentException()
            };

            SystemsContainer.Register<ISubSystem, T>(reuse);
            SystemsContainer.RegisterInitializer<ISubSystem>((subSystem, resolver) =>
                Logger.Log().Information(Resources.WithAudioBackendMessage, subSystem.ToString()));

            return this;
        }

        /// <summary>
        /// Configures and build the application.
        /// </summary>
        public virtual void ConfigureAndBuild()
        {
            SystemsContainer.RegisterInitializer<ISubSystem>((subSystem, resolver) => subSystem.StartUp());
            SystemsContainer.RegisterDisposer<IDisposable>(coreSystem => coreSystem.Dispose());

            IsSuccessfullyBuilt = true;
        }

        /// <summary>
        /// Runs the application if it is successfully build.
        /// Otherwise it breaks the application.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public virtual void Run()
        {
            if (!IsSuccessfullyBuilt)
            {
                throw new ApplicationException(Resources.ProgramNotBuiltMessage);
            }

            SystemsContainer.Resolve<GameSystem>().Run();
        }

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
                SystemsContainer.Dispose();
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Checks if the window is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckWindowCompatability<T>() where T : IProgramWindow;

        /// <summary>
        /// Checks if the garphics backend is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckGraphicsBackendCompatability<T>() where T : GraphicsAPI;

        /// <summary>
        /// Checks if the audio backend is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckAudioBackendCompatability<T>() where T : AudioAPI;

        /// <summary>
        /// Checks if the input is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckInputCompatability<T>() where T : IInputSystem;
    }
}

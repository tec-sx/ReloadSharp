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
using Reload.Core.Audio;
using Reload.Core.Configuration;
using Reload.Core.Graphics;
using Reload.Core.Input;
using System;

namespace Reload.Core.Game
{
    /// <summary>
    /// The base game system class.
    /// Can be instantiated through <see cref="GameBuilder"/>
    /// </summary>
    public abstract class GameSystem : ICoreSystem, IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; init; }

        #region Core Systems
            
        /// <summary>
        /// Gets the game window.
        /// </summary>
        public IProgramWindow Window { get; internal init; }

        /// <summary>
        /// Gets the graphics API used.
        /// </summary>
        public GraphicsAPI Graphics { get; internal init; }

        /// <summary>
        /// Gets or sets the audio system.
        /// </summary>
        public AudioAPI Audio { get; internal set; }

        /// <summary>
        /// Gets the input system.
        /// </summary>
        public IInputSystem Input { get; internal init; }

        #endregion

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

        /// <summary>
        /// Itterates throug all included sub-systems and calls the <see cref="ICoreSystem.StartUp"/>
        /// for each of them. afterwards it calls the <see cref="OnInitialize"/> method.
        /// </summary>
        public void StartUp()
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
                Input.Dispose();
                Audio.Dispose();
                Graphics.Dispose();
                Window.Dispose();
            }

            _isDisposed = true;
        }
    }
}

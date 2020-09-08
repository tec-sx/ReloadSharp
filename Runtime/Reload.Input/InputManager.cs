using Reload.Core.Input;
using Silk.NET.Input;
using Silk.NET.Input.Common;
using Silk.NET.Windowing.Common;
using System;
using System.Collections.Generic;

namespace Reload.Input
{
    public class InputManager : IInputSystem
    {
        private bool _disposed;

        public IInputContext Context;

        public IReadOnlyList<IKeyboard> Keyboards => Context.Keyboards;

        public IReadOnlyList<IMouse> Mices => Context.Mice;

        public InputHandler Handler { get; }

        public InputSourceType Source => throw new NotImplementedException();

        public InputManager()
        {
            Handler = new InputHandler();
        }

        public void Update(double deltaTime)
        {
            Handler.Update(deltaTime);
        }

        /// <summary>
        /// Attach game input. call on window load.
        /// </summary>
        public void Initialize(IWindow window)
        {
            Context = window.CreateInput();
            Handler.Attach(Context);
        }

        /// <inheritdoc/>
        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if(disposing)
            {
                Handler.Detach(Context);
                Context?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void StartUp()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void ShutDown()
        {
            throw new NotImplementedException();
        }
    }
}
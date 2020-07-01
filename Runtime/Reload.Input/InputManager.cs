namespace Reload.Input
{
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Collections.Generic;

    public class InputManager : IDisposable
    {
        public IInputContext Context;

        public IReadOnlyList<IKeyboard> Keyboards => Context.Keyboards;

        public IReadOnlyList<IMouse> Mices => Context.Mice;

        public InputHandler Handler { get; }

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

        public void Dispose()
        {
            Handler.Detach(Context);
            Context?.Dispose();
        }
    }
}
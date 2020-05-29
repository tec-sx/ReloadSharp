namespace Reload.Engine.Input
{
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Collections.Generic;

    public class InputManager : IDisposable
    {
        public IInputContext InputContext;

        public IReadOnlyList<IKeyboard> Keyboards => InputContext.Keyboards;

        public IReadOnlyList<IMouse> Mices => InputContext.Mice;

        public InputHandler Handler { get; }

        public InputManager()
        {
            Handler = new InputHandler();
        }

        public void Update()
        {
            Handler.Update();
        }

        /// <summary>
        /// Attach game input. call on window load.
        /// </summary>
        public void Initialize(IWindow window)
        {
            InputContext = window.CreateInput();
            Handler.Attach(InputContext);
        }

        public void Dispose()
        {
            Handler.Detach(InputContext);
            InputContext?.Dispose();
        }
    }
}
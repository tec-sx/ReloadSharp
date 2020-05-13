namespace Reload.Input.Source
{
    using Reload.Game;
    using Silk.NET.Input.Common;
    using Silk.NET.Input.Extensions;
    using Silk.NET.Windowing.Common;
    using System;

    internal class InputSourceSilk : InputSourceBase
    {
        private GameContext<IWindow> windowContext;
        private IWindow windowHandle;
        private IMouse mouse;
        private IKeyboard keyboard;
        private IJoystick joystick;
        private InputManager inputManager;

        public override void Initialize(InputManager inputManager)
        {
            this.inputManager = inputManager;
            windowContext = inputManager.Game
            windowHandle = windowContext.Handle;
        }

    }
}

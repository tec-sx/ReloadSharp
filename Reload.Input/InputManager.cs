namespace Reload.Input
{
    using Reload.Game;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputManager : IDisposable
    {
        public IInputContext InputContext;

        private readonly GameBase _game;

        public IReadOnlyList<IKeyboard> Keyboards => InputContext.Keyboards;

        public IReadOnlyList<IMouse> Mices => InputContext.Mice;

        public InputHandler Handler { get; }

        public InputManager(IGame game)
        {
            _game = game as GameBase;
            Handler = new InputHandler();


        }

        public void Update()
        {
            Handler.Update();
        }

        /// <summary>
        /// Attach game input. call on window load.
        /// </summary>
        public void Initialize()
        {
            InputContext = _game.Window.CreateInput();
            Handler.Attach(InputContext);

            _game.Activated += OnApplicationResumed;
            _game.Deactivated += OnApplicationPaused;
        }

        private void OnApplicationPaused()
        {
        }

        private void OnApplicationResumed()
        {
        }

        public void Dispose()
        {
            _game.Activated -= OnApplicationResumed;
            _game.Deactivated -= OnApplicationPaused;

            Handler.Detach(InputContext);
            InputContext?.Dispose();
        }
    }
}
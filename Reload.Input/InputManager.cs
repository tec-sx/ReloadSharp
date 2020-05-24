namespace Reload.Input
{
    using Reload.Game;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputManager
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
        /// Initialize game input. call on window load.
        /// </summary>
        public void Load()
        {
            InputContext = _game.Window.CreateInput();
            Handler.Initialize(InputContext.Keyboards, InputContext.Mice);
           
            _game.Activated += OnApplicationResumed;
            _game.Deactivated += OnApplicationPaused;
        }

        public void ShutDown()
        {
            InputContext?.Dispose();
        }

        private void OnApplicationPaused(object sender, EventArgs e)
        {
        }

        private void OnApplicationResumed(object sender, EventArgs e)
        {
        }
    }
}
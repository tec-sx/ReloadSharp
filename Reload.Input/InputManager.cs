namespace Reload.Input
{
    using Reload.Game;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputManager
    {
        private IInputContext _context;

        private readonly GameBase _game;

        public IReadOnlyList<IKeyboard> Keyboards => _context.Keyboards;

        public IReadOnlyList<IMouse> Mices => _context.Mice;

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

        public void Initialize()
        {
            _game.Window.Load += () =>
            {
                _context = _game.Window.CreateInput();

                Handler.Initialize(_context.Keyboards, _context.Mice);
            };

            _game.Activated += OnApplicationResumed;
            _game.Deactivated += OnApplicationPaused;
        }

        private void OnApplicationPaused(object sender, EventArgs e)
        {
        }

        private void OnApplicationResumed(object sender, EventArgs e)
        {
        }

    }
}

namespace Reload.Input
{
    using Reload.Game;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class ReloadInput
    {
        private IInputContext _context;

        private readonly GameBase _game;

        public IReadOnlyList<IKeyboard> Keyboards => _context.Keyboards;

        public IReadOnlyList<IMouse> Mices => _context.Mice;

        public ReloadInputHandler Handler { get; }

        public ReloadInput(IGame game)
        {
            _game = game as GameBase;

            Handler = new ReloadInputHandler();
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

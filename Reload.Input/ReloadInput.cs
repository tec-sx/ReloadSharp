namespace Reload.Input
{
    using Reload.Core;
    using Reload.Game;
    using Reload.Input.Configuration;
    using Reload.Input.Source;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using Silk.NET.Input.Extensions;
    using System;
    using System.Collections.Generic;

    public class ReloadInput
    {
        private IInputContext context;

        public GameBase Game { get; }

        public ReloadInputHandler Handler { get; private set; }


        public ReloadInput(IGame game)
        {
            Game = game as GameBase;
        }

        public void Update()
        {

        }

        public void Initialize(
            KeyboardConfiguration keyboardConfiguration,
            MouseConfiguration mouseConfiguration)
        {
            Game.Window.Load += () =>
            {
                context = Game.Window.CreateInput();
                //keyboard = new ReloadKeyboard(inputContext.Keyboards[0]);
                //mouse = new Mouse(inputContext.Mice[0], Game);
            };

            Game.Activated += OnApplicationResumed;
            Game.Deactivated += OnApplicationPaused;
        }

        public void RegisterCommand(Key control, Command command) => commands.Add(control, command);

        private void OnApplicationPaused(object sender, EventArgs e)
        {
        }

        private void OnApplicationResumed(object sender, EventArgs e)
        {
        }

    }
}

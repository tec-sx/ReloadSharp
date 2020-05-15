namespace Reload.Input
{
    using Reload.Core;
    using Reload.Game;
    using Reload.Input.Configuration;
    using Reload.Input.Contexts;
    using Reload.Input.Source;
    using Silk.NET.Input;
    using System;

    public class ReloadInputManager
    {
        public event Action<Command> CommandFired;

        private InputContextBase currentContext;

        public GameBase Game { get; }
        public ReloadKeyboard Keyboard { get; private set; }
        public Mouse Mouse { get; private set; }
        public ITextInputDevice TextInput { get; private set; }

        public ReloadInputManager(IGame game)
        {
            Game = game as GameBase;
        }

        private void Update(double deltaTime)
        {
            Keyboard.Update(deltaTime);
            Mouse.Update(deltaTime);
        }

        public void InitializeDevices()
        {
            var input = Game.Window.CreateInput();

            Keyboard = new ReloadKeyboard(input.Keyboards[0], this);
            Mouse = new Mouse(input.Mice[0], Game);

            TextInput = Keyboard as ITextInputDevice;
        }

        public void Initialize(
            KeyboardConfiguration keyboardConfiguration,
            MouseConfiguration mouseConfiguration)
        {
            Game.Window.Load += InitializeDevices;
            Game.Window.Update += Update;

            Game.Activated += OnApplicationResumed;
            Game.Deactivated += OnApplicationPaused;
        }

        public void FireCommand(Command command)
        {
            CommandFired?.Invoke(command);
        }

        private void OnApplicationPaused(object sender, EventArgs e)
        {
        }

        private void OnApplicationResumed(object sender, EventArgs e)
        {
        }
    }
}

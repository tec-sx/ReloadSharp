namespace Reload.Input
{
    using Reload.Game;
    using Reload.Input.Configuration;
    using Reload.Input.Source;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using System;

    public class ReloadInputManager
    {
        public event Action InputDevicesInitialized;

        private IInputContext inputContext;

        public GameBase Game { get; }
        public Keyboard Keyboard { get; private set; }
        public Mouse Mouse { get; private set; }
        public ITextInputDevice TextInput { get; private set; }

        public ReloadInputManager(IGame game)
        {
            Game = game as GameBase;
            Game.Window.Load += InitializeDevices();
        }

        public Action InitializeDevices()
        {
            inputContext = Game.Window.CreateInput();

            Keyboard = new Keyboard(inputContext.Keyboards[0]);
            Mouse = new Mouse(inputContext.Mice[0], Game);

            TextInput = Keyboard as ITextInputDevice;

            return InputDevicesInitialized;
        }

        public void Initialize(
            KeyboardConfiguration keyboardConfiguration,
            MouseConfiguration mouseConfiguration)
        {
            Game.Activated += OnApplicationResumed;
            Game.Deactivated += OnApplicationPaused;
        }

        private void OnApplicationPaused(object sender, EventArgs e)
        {
        }

        private void OnApplicationResumed(object sender, EventArgs e)
        {
        }
    }
}

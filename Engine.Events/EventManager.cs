namespace Engine.Events
{
    using Engine.Events.Keyboard;
    using Engine.Events.Mouse;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using Silk.NET.Windowing.Common;

    public class EventManager : IEventManager
    {
        public KeyboardEvent KeyEvent { get; private set; }

        public MouseEvent MouseEvent { get; private set; }

        public void Initialize(
            IWindow window,
            KeyboardConfiguration keyboardConfiguration,
            MouseConfiguration mouseConfiguration)
        {
            window.Load += () =>
            {
                IInputContext input = window.CreateInput();

                MouseEvent = new MouseEvent(input.Mice[0]);
                KeyEvent = new KeyboardEvent(input.Keyboards[0]);

                MouseEvent.MapInput(mouseConfiguration);
                KeyEvent.MapInput(keyboardConfiguration);
            };
        }
    }
}

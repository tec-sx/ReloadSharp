namespace Engine.Input
{
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using Silk.NET.Windowing.Common;

    public class InputManager : IEventManager
    {
        public IMouse Mouse { get; private set; }
        public IKeyboard Keyboard { get; private set; }

        public void Initialize(IWindow window)
        {
            window.Load += () =>
            {
                var input = window.CreateInput();

                Mouse = input.Mice[0];
                Keyboard = input.Keyboards[0];
            };
        }
    }
}
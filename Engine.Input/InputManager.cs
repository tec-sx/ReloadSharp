namespace Engine.Input
{
    using Silk.NET.Input;
    using Silk.NET.Windowing.Common;
    using Silk.NET.Input.Common;

    public class InputManager
    {
        public IMouse Mouse { get; private set; }
        public IKeyboard Keyboard { get; private set; }

        public void Initialize(IView view)
        {
            var input = view.CreateInput();

            Mouse = input.Mice[0];
            Keyboard = input.Keyboards[0];
        }
    }
}
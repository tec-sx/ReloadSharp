namespace Core.CoreSystem.Input
{
    using Silk.NET.Input;
    using Controllers;
    using Silk.NET.Windowing.Common;
    
    public class InputManager : IInputManager
    {
        public Mouse Mouse { get; }
        public Keyboard Keyboard { get; }
        
        public InputManager(IView view)
        {
            var input = view.CreateInput();
            
            Mouse = new Mouse(input.Mice);
            Keyboard = new Keyboard(input.Keyboards);
        }
    }
}
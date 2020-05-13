namespace Engine.Events
{
    using Engine.Events.Keyboard;
    using Engine.Events.Mouse;
    using Silk.NET.Windowing.Common;

    public interface IEventManager
    {
        KeyboardEvent KeyEvent { get; }
        MouseEvent MouseEvent { get; }
        void Initialize(IWindow window,
            KeyboardConfiguration keyboardConfiguration,
            MouseConfiguration mouseConfiguration);
    }
}

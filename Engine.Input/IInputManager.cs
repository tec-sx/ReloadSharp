namespace Engine.Input
{
    using Silk.NET.Input.Common;
    using Silk.NET.Windowing.Common;

    public interface IEventManager
    {
        public IMouse Mouse { get; }
        public IKeyboard Keyboard { get; }
        void Initialize(IWindow window);
    }
}

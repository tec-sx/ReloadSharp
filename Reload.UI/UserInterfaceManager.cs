namespace Reload.UI
{
    using System.Drawing;
    using Graphics;
    using Game;
    using Input;
    using Ultz.SilkExtensions.ImGui;

    public class UserInterfaceManager
    {
        private ImGuiController _controller;
        private readonly IGame _game;
        private readonly GraphicsManager _graphics;
        private readonly InputManager _input;
        private readonly DebugUi _debugLayer;

        public UserInterfaceManager(IGame game, GraphicsManager graphics, InputManager input)
        {
            _game = game;
            _graphics = graphics;
            _input = input;
#if DEBUG
            _debugLayer = new DebugUi(_game);
#endif
        }

        public void Load()
        {
            var gl = _graphics.Gl;
            var window = _game.Window;
            var inputContext = _input.InputContext;
            _controller = new ImGuiController(gl, window, inputContext);
        }

        public void Update(double deltaTime)
        {
            _controller.Update((float)deltaTime);
        }

        public void Render(double deltaTime)
        {
#if DEBUG
            _debugLayer.Draw(deltaTime);
#endif
            _controller.Render();
        }

        public void Resize(Size size)
        {
        }

        public void ShutDown()
        {
            _controller?.Dispose();
        }
    }
}
using System.Drawing;
using ImGuiNET;
using Reload.Core.Collections;
using Reload.Graphics;
using Reload.Game;
using Reload.Input;
using Ultz.SilkExtensions.ImGui;

namespace Reload.UI
{
    public class UserInterfaceManager
    {
        private ImGuiController _controller;
        private readonly IGame _game;
        private readonly GraphicsManager _graphics;
        private readonly InputManager _input;

        private readonly FastList<IUserInterface> _uiLayers;
        private readonly DebugUi _debugLayer;

        public UserInterfaceManager(IGame game, GraphicsManager graphics, InputManager input)
        {
            _game = game;
            _graphics = graphics;
            _input = input;
            _uiLayers = new FastList<IUserInterface>();

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
            for (var i = 0; i < _uiLayers.Count; i++)
            {
                _uiLayers[i].Draw(deltaTime);
            }
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
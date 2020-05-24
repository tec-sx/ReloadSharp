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

        private FastList<IUserInterface> _uiLayers;
        private DebugUi _debugLayer;

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
            var gl = _graphics.GlApi;
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
            ImGui.BeginMainMenuBar();

            for (var i = 0; i < _uiLayers.Count; i++)
            {
                _uiLayers[i].Draw();
            }
#if DEBUG
            _debugLayer.Draw();
#endif
            ImGui.End();

            _controller.Render();
        }

        public void ShutDown()
        {
            _controller?.Dispose();
        }
    }
}
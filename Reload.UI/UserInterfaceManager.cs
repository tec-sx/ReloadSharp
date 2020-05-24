using System;
using Reload.Graphics;
using Reload.Game;
using Reload.Input;
using Silk.NET.Input.Common;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Common;
using Ultz.SilkExtensions.ImGui;

namespace Reload.UI
{
    public class UserInterfaceManager
    {
        private ImGuiController _controller;
        private readonly IGame _game;
        private readonly GraphicsManager _graphics;
        private readonly InputManager _input;
        
        public UserInterfaceManager(IGame game, GraphicsManager graphics, InputManager input)
        {
            _game = game;
            _graphics = graphics;
            _input = input;
        }

        public void Load()
        {
            var gl = _graphics.GlApi;
            var window = _game.Window;
            var inputContext = _input.InputContext;
            _controller = new ImGuiController(gl, window, inputContext);
        }

        public void Render()
        {
            ImGuiNET.ImGui.ShowDemoWindow();
            _controller.Render();
        }

        public void ShutDown()
        {    
            _controller?.Dispose();
        }
    }
}
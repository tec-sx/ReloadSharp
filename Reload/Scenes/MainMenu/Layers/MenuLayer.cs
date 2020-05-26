using System.IO;
using Reload.Configuration;

namespace Reload.Scenes.MainMenu.Layers
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;
    using Scene.Enumerations;
    using ImGuiNET;
    using Reload.Scene.Layers;

    public struct MenuLayout
    {
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 ButtonSize { get; set; }
    }

    public class MenuLayer : LayerBase
    {
        private MenuLayout _layout;
        private bool _windowStyleSet;
        public Dictionary<string, ImFontPtr> Fonts { get; private set; }

        public override void OnAttach()
        {
            var gameWindow = Scene.SceneManager.Game.Window;

            gameWindow.Resize += OnResize;

            _layout.Size = new Vector2(800, 600);
            _layout.Position = CalculateMenuPosition(gameWindow.Size.Width, gameWindow.Size.Height);
            _layout.ButtonSize = new Vector2(_layout.Size.X, 80);

            Fonts = new Dictionary<string, ImFontPtr>();

            var io = ImGui.GetIO();

            var orbitron = Path.Combine(ContentPaths.Fonts, "Orbitron.ttf");
            Fonts.Add(orbitron, io.Fonts.AddFontFromFileTTF(orbitron, 20));
        }

        public override void OnDetach()
        {
        }

        public override void Update(double deltaTime)
        {

        }

        private void OnResize(Size size)
        {
            _layout.Position = CalculateMenuPosition(size.Width, size.Height);
        }

        public override void Draw(double deltaTime)
        {
            if (!_windowStyleSet)
            {
                var style = ImGui.GetStyle();

                style.WindowRounding = 0f;
                style.WindowMinSize = _layout.Size;

                style.WindowMenuButtonPosition = ImGuiDir.COUNT;

                _windowStyleSet = true;
            }

            var menuFlags = ImGuiWindowFlags.NoDecoration;

            if (ImGui.Begin("Main Menu", menuFlags))
            {
                if (Fonts.TryGetValue("Orbitron.ttf", out var font))
                {
                    ImGui.PushFont(font);
                }

                ImGui.SetWindowPos(_layout.Position);

                ImGui.Text("Reload");

                ImGui.Button("Start", _layout.ButtonSize);
                ImGui.Button("Settings", _layout.ButtonSize);

                if (ImGui.Button("Exit", _layout.ButtonSize))
                {
                  Scene.ChangeSceneState(SceneState.ExitProgram);
                }

                ImGui.EndMenu();
            }

        }

        private Vector2 CalculateMenuPosition(int screenWidth, int screenHeight)
        {
            return new Vector2(
                (float) screenWidth / 2 - _layout.Size.X / 2,
                (float) screenHeight / 2 - _layout.Size.Y / 2);
        }
    }
}

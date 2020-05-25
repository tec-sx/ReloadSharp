using System.Numerics;

namespace Reload.Scenes.MainMenu.Layers
{
    using ImGuiNET;
    using Reload.Scene.Layers;
    using UI;

    public class MenuLayer : LayerBase
    {
        public override void OnAttach()
        {
        }

        public override void OnDetach()
        {
        }

        public override void Update(double deltaTime)
        {
        }

        public override void Draw(double deltaTime)
        {
            var style = ImGui.GetStyle();

            style.WindowRounding = 0f;
            style.WindowMinSize = new Vector2(400, 200);
            style.WindowPadding = new Vector2(50, 50);

            var menuFlags = ImGuiWindowFlags.NoDecoration;
            ImGui.Begin("Main menu", menuFlags);
            ImGui.Button("Start");
            ImGui.EndMenu();
        }
    }
}

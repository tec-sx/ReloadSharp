namespace Reload.Editor.Scenes.Layers.Components
{
    using ImGuiNET;
    using Reload.UI;

    public class MenuBarComponent : UiWindow
    {
        public override void Draw()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File", true))
                {
                    if (ImGui.MenuItem("Exit", true))
                    {
                        Program.Editor.Window.Close();
                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }

        public override void Show()
        {
            show = true;
        }
    }
}

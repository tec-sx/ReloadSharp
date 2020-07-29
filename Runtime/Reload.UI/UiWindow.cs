using ImGuiNET;

namespace Reload.UI
{
    public abstract class UiWindow
    {
        protected bool show;

        protected UiWindow()
        {
            show = true;
        }

        protected virtual bool Begin(string name)
        {
            return ImGui.Begin(name, ref show);
        }

        protected virtual void End()
        {
            ImGui.End();
        }

        public abstract void Draw(double deltaTime);
        public abstract void Show();
    }
}

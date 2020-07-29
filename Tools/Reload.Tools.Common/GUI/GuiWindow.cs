namespace Reload.Tools.Common.GUI
{
    public abstract class GuiWindow
    {
        protected bool show;

        protected GuiWindow()
        {
            show = true;
        }

        protected virtual bool Begin(string name)
        {
            // return ImGui.Begin(name, ref show);
            return true;
        }

        protected virtual void End()
        {
            // ImGui.End();
        }

        public abstract void Draw(double deltaTime);
        public abstract void Show();
    }
}

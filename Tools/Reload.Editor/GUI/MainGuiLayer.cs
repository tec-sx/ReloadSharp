namespace Reload.Editor.Scenes.Layers
{
    using Reload.Editor.Scenes.Layers.Components;
    using Reload.UI;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    public class MainGuiLayer
    {
        private IWindow _window;
        private UiManager _uiManager;

        public MainGuiLayer(UiManager uiManager, IWindow window)
        {
            _uiManager = uiManager;
            _window = window;

            _window.Resize += OnResize;

            _uiManager.AddWindow(new MenuBarComponent());
            _uiManager.AddWindow(new RightAsideComponent());
        }

        public void OnResize(Size size)
        {

        }

        public void OnDetach()
        {
            _window.Resize -= OnResize;
        }

        public void Update(double deltaTime)
        {
            _uiManager.Update(deltaTime);
        }

        public void Render(double deltaTime)
        {
            _uiManager.Render(deltaTime);
        }
    }
}

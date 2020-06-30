namespace Reload.Editor
{
    using Reload.Editor.Scenes;
    using Reload.Editor.Scenes.Layers;
    using Reload.Engine;
    using Reload.Rendering;
    using System.Drawing;

    public class GameEditor : Game
    {
        private MainGuiLayer _mainGui;

        public GameEditor(string[] args)
            : base(args)
        { }

        protected override void OnInitialize()
        {
            _mainGui = new MainGuiLayer(UiManager, Window);
            
            SceneMachine.AddScene<MainViewport>();

            Renderer.Initialize();
        }

        protected override void OnLoadContent()
        {
        }

        protected override void OnRender(double deltaTime)
        {
            SceneMachine.Render(deltaTime);

            _mainGui.Render(deltaTime);
        }

        protected override void OnShutDown()
        {
            _mainGui.OnDetach();
        }

        protected override void OnUpdate(double deltaTime)
        {
            SceneMachine.Update(deltaTime);

            _mainGui.Update(deltaTime);
        }
    }
}

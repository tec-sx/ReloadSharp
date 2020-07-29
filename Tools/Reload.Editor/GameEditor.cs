namespace Reload.Editor
{
    using Reload.Editor.Scenes;
    using Reload.Editor.Scenes.Layers;
    using Reload.Engine;
    using Reload.Rendering;
    using System.Drawing;

    public class GameEditor : Game
    {
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        
        public GameEditor(string[] args)
            : base(args)
        { }

        public void Initialize(int viewportWidth, int viewportHeight)
        {
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
            OnInitialize();
        }

        protected override void OnInitialize()
        {
            SceneMachine.AddScene<MainViewport>();
            Renderer.Initialize();
            SceneMachine.Run();
        }

        protected override void OnLoadContent()
        { }

        public void Render(double deltaTime) => OnRender(deltaTime);
        protected override void OnRender(double deltaTime)
        {
            SceneMachine.Render(deltaTime);
        }

        public void ShutDown() => OnShutDown();
        protected override void OnShutDown()
        { }

        public void Update(double deltaTime) => OnUpdate(deltaTime);
        protected override void OnUpdate(double deltaTime)
        {
            SceneMachine.Update(deltaTime);
        }
    }
}

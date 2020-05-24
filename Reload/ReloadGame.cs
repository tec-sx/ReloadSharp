namespace ReloadGame
{
    using Reload.Engine;
    using Scenes;

    public class ReloadGame : Game
    {
        public ReloadGame(string[] args) : base(args)
        {}

        protected override void OnInitialize()
        {
            SceneManager.AddScene<IntroScene>();
        }

        protected override void OnLoadContent()
        {
        }

        protected override void OnUpdate(double deltaTime)
        {
        }

        protected override void OnRender(double deltaTime)
        {
        }

        protected override void OnShutDown()
        {

        }
    }
}
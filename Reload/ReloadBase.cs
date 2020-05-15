namespace ReloadGame
{
    using Reload.Engine;
    using Scenes;

    public class ReloadBase : Game
    {
        public ReloadBase()
        {}

        protected override void OnInitialize()
        {
            var introScreen = SceneManager.AddScene<IntroScene>();
            SceneManager.ActiveScene = introScreen;
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
namespace ReloadGame
{
    using global::Reload.Engine;
    using Scenes;

    public class Reload : Game
    {
        public Reload()
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
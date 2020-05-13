namespace Reload
{
    using Game;
    using Scenes;

    public class Reload : GameBase
    {
        public Reload(string[] args)
            : base(args)
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
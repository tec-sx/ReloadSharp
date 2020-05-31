namespace Reload.Game
{
    using Reload.Game.Scenes;

    public class ReloadGame : Engine.Game
    {
        public ReloadGame(string[] args) : base(args)
        {}

        protected override void OnInitialize()
        {
            SceneMachine.AddScene<MainMenuScene>();
            SceneMachine.AddScene<IntroScene>();
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
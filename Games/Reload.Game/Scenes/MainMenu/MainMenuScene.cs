namespace Reload.Game.Scenes
{
    using Reload.Assets.Audio.Models;
    using Reload.Scenes.MainMenu.Layers;
    using Reload.Engine.SceneSystem;


    public class MainMenuScene : Scene
    {
        private IMusic _bgMusicStream;

        public override void OnEnter()
        {
            Layers.PushLayer<MenuLayer>();

            _bgMusicStream = SceneManager.Assets.LoadMusic("Intro");
            //_bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
            Layers.ClearStack();
        }

        public override void OnRender(double deltaTime)
        {

        }

        public override void OnUpdate(double deltaTime)
        {
        }
    }
}

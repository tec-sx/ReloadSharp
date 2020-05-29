namespace ReloadGame.Scenes
{
    using Reload.AssetPipeline.Audio.Models;
    using Reload.Scenes.MainMenu.Layers;
    using Reload.Scene;

    public class MainMenuScene : SceneBase
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

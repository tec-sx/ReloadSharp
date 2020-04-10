using Core.AssetsPipeline.Audio.Models;

namespace Reload.Screens
{
    using Core.AssetsPipeline.Textures;
    using Core.Screen;

    public class IntroScreen : ScreenBase
    {
        private IMusic _bgMusicStream;

        public override void OnEnter()
        {
            _bgMusicStream = Manager.Assets.LoadMusic("Intro");
            _bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
        }

        public override void OnUpdate(double deltaTime)
        {
            _bgMusicStream.Update();
        }

        public override void OnRender(double deltaTime)
        {
        }
    }
}
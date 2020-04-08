namespace Reload.Screens
{
    using Core.Resources.Audio;
    using Core.Resources.Textures;
    using Core.Screen;

    public class IntroScreen : ScreenBase
    {
        private IMusic _bgMusicStream;

        public override void OnEnter()
        {
            _bgMusicStream = Manager.Assetses.LoadMusic("Intro");
            _bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
        }

        public override void OnUpdate()
        {
            _bgMusicStream.Update();
        }

        public override void OnRender()
        {
        }
    }
}
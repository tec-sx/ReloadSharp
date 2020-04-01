namespace Reload.Screens
{
    using Core.Resources.Audio;
    using Core.Resources.Textures;
    using Core.Screen;

    public class IntroScreen : ScreenBase
    {
        private IMusic _bgMusicStream;
        private ITexture _texture;

        public override void OnEnter()
        {
            _bgMusicStream = Manager.Resources.LoadMusic("Intro");
            _bgMusicStream.Play();

            _texture = Manager.Resources.GetTexture("Player");
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
        }

        public override void Update()
        {
            _bgMusicStream.Update();
        }

        public override void Render()
        {
            _texture.Render(0, 0, 0, 0);
        }
    }
}
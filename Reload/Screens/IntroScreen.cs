using System.IO;
using Core.Audio;
using Raylib_cs;

namespace Reload.Screens
{
    using Core.Screen;

    public class IntroScreen : ScreenBase
    {
        private MusicStream _bgMusicStream;
        private Texture2D _texture;
        
        public override void OnEnter()
        {
            _bgMusicStream = Manager.Audio.LoadMusic("Intro");
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
            Raylib.DrawTexture(
                _texture,
                0,
                0,
                Color.WHITE);
        }
    }
}
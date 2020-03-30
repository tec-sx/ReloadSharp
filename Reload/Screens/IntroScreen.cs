using System.IO;
using Core.Audio;

namespace Reload.Screens
{
    using Core.Screen;

    public class IntroScreen : ScreenBase
    {
        private MusicStream _bgMusicStream;
        
        public override void OnEnter()
        {
            _bgMusicStream = audioEngine.LoadMusic("Intro");
            _bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
            audioEngine.Dispose();
        }

        public override void Update()
        {
            _bgMusicStream.Update();
        }

        public override void Render()
        {
        }
    }
}
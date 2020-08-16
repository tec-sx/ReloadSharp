namespace Reload.Audio.Tests
{
    using System;
    using Reload.Audio;
    using FluentAssertions;
    using Xunit;
    using System.IO;

    public class ReloadAudioTests : IDisposable
    {
        private readonly AudioSystem _audioManager = new AudioSystem();

        public void Dispose() => _audioManager.ShutDown();

        [Fact]
        public void Play_Music_Stream_Stereo_Success()
        {
            var music = new AudioSource(File.OpenRead("Assets/Background.Original.ogg"));

            music.Should().NotBeNull();

            music.Play(loop: false);
            while (music.Elapsed.Seconds < music.Duration.Seconds) ;
            music.Stop();
        }

        [Fact]
        public void Play_Sound_SingleShot_Success()
        {
            var data = File.ReadAllBytes("Assets/Snare.ogg");
            var snare = new AudioSource(new MemoryStream(data));

            snare.Should().NotBeNull();

            snare.Play(loop: false);
            while (snare.Elapsed.Seconds < snare.Duration.Seconds) ;
            snare.Stop();
        }

        [Fact]
        public void Play_Sound_Loop_Success()
        {
            var data = File.ReadAllBytes("Assets/Snare.ogg");
            var snare = new AudioSource(new MemoryStream(data));

            snare.Should().NotBeNull();

            snare.Play(loop: true);
            snare.Looping.Should().BeTrue();

            while (snare.Elapsed.Seconds < 2) ;
            snare.Stop();
        }

        [Fact]
        public void Sound_Gain_Change_Success()
        {
            var data = File.ReadAllBytes("Assets/Snare.ogg");
            var snare = new AudioSource(new MemoryStream(data));

            snare.Should().NotBeNull();

            var downBeat = 1;
            var gainIsLowering = true;

            snare.Gain = 1.0f;
            snare.Play(loop: true);

            while (snare.Elapsed.Seconds < 4)
            {
                if (snare.Elapsed.Seconds == downBeat)
                {
                    if (gainIsLowering)
                    {
                        snare.Gain -= 10f;
                        snare.Gain.Should().Be(0.001f);
                    }
                    else
                    {
                        snare.Gain += 10f;
                        snare.Gain.Should().Be(1.0f);
                    }

                    gainIsLowering = !gainIsLowering;
                    downBeat += 1;
                }
            }

            snare.Stop();
        }
    }
}

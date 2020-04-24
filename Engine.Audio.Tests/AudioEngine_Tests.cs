namespace Engine.Audio.Tests
{
    using NUnit.Framework;
    using FluentAssertions;
    using Engine.Audio;

    public class AudioEngine_Tests
    {
        private SoundManager _soundManager;

        [OneTimeSetUp]
        public void SetUp()
        {
            new AudioDevice();
            _soundManager = new SoundManager();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _soundManager.DisposeResources();
        }

        [Test]
        public void Play_Music_Stream_Stereo_Success()
        {
            var music = _soundManager.LoadMusic("Assets/Background.Original.ogg");
            music.Should().NotBeNull();

            music.Play();
            while (music.Elapsed.Seconds < music.Duration.Seconds) ;
            music.Stop();
        }

        [Test]
        public void Play_Sound_SingleShot_Success()
        {
            var snare = _soundManager.LoadSound("Assets/Snare.ogg");
            snare.Should().NotBeNull();

            snare.Play();
            while (snare.Elapsed.Seconds < snare.Duration.Seconds) ;
            snare.Stop();
        }

        [Test]
        public void Play_Sound_Loop_Success()
        {
            var snare = _soundManager.LoadSound("Assets/Snare.ogg");
            snare.Should().NotBeNull();

            snare.Play(loop: true);
            snare.Looping.Should().BeTrue();

            while (snare.Elapsed.Seconds < 2);
            snare.Stop();
        }

        [Test]
        public void Sound_Gain_Change_Success()
        {
            var snare = _soundManager.LoadSound("Assets/Snare.ogg");
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

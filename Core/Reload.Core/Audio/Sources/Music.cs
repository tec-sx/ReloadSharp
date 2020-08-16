using Reload.Core.Audio;
using System;

namespace Reload.Audio
{
    public class Music
    {
        private readonly AudioSource _source;

        public float Gain
        {
            get => _source.Gain;
            set => _source.Gain = value;
        }

        public TimeSpan Duration => _source.Duration;
        public TimeSpan Elapsed => _source.Elapsed;

        public Music(AudioSource source)
        {
            _source = source;
        }

        public void Play()
        {
            _source.Play(loop: false);
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            _source.Stop();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }
    }
}
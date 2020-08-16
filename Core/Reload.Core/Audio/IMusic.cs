using System;

namespace Reload.Core.Audio
{
    public interface IMusic
    {
        public float Gain { get; set; }
        public TimeSpan Duration { get; }
        public TimeSpan Elapsed { get; }

        void Play();
        void Pause();
        void Stop();
        void Resume();
    }
}

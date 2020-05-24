namespace Reload.AssetPipeline.Audio.Models
{
    using System;

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

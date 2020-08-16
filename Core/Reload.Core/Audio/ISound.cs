using System.Numerics;

namespace Reload.Core.Audio
{
    public interface ISound
    {
        public float Gain { get; set; }
        public bool Looping { get; }
        public float Pitch { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public Vector3 Velocity { get; set; }
        void Play(bool loop);
        void Stop();
    }
}

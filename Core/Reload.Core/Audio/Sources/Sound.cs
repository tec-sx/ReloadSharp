using System.Numerics;

namespace Reload.Core.Audio
{
    public class Sound
    {
        private readonly AudioSource _source;

        public float Gain
        {
            get => _source.Gain;
            set => _source.Gain = value;
        }

        public float Pitch
        {
            get => _source.Pitch;
            set => _source.Pitch = value;
        }

        public bool Looping => _source.Looping;

        public Vector3 Position
        {
            get => _source.Position;
            set => _source.Position = value;
        }
        public Vector3 Direction
        {
            get => _source.Direction;
            set => _source.Direction = value;
        }

        public Vector3 Velocity
        {
            get => _source.Velocity;
            set => _source.Velocity = value;
        }

        public Sound(AudioSource source)
        {
            _source = source;
        }

        public void Play(bool loop = false) => _source.Play(loop);
        public void Stop() => _source.Stop();
    }
}
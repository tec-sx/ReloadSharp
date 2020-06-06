namespace Reload.Audio
{
    using Reload.Audio.Backend;
    using Silk.NET.OpenAL;

    public sealed class AudioManager
    {
        public AudioContext Context { get; private set; }

        public AudioManager()
        {
            Context = new AudioContext();
            ALNative.SetDistanceModel(DistanceModel.InverseDistanceClamped);
        }

        public void ShutDown()
        {
            Context?.Dispose();
        }
    }
}

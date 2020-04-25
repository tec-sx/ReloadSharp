namespace Engine.Audio
{
    using Engine.Audio.Backend;
    using Silk.NET.OpenAL;

    public sealed class AudioManager
    {
        public AudioContext Context { get; private set; }

        public void CreateContext()
        {
            Context = new AudioContext();

            ALNative.SetDistanceModel(DistanceModel.InverseDistanceClamped);
        }

        public void DisposeResources()
        {
            Context?.Dispose();
        }
    }
}

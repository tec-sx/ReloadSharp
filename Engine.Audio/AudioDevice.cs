namespace Engine.Audio
{
    using Engine.Audio.Backend;
    using Silk.NET.OpenAL;
    using System.Collections.Generic;

    public class AudioDevice
    {
        private AudioContext _context;

        public AudioDevice()
        {
            _context = new AudioContext();

            ALNative.SetDistanceModel(DistanceModel.InverseDistanceClamped);
        }
    }
}

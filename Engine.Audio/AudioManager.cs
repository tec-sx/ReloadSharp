﻿namespace Engine.Audio
{
    using Engine.Audio.Backend;
    using Silk.NET.OpenAL;

    public sealed class AudioManager : IAudioManager
    {
        public AudioContext Context { get; private set; }

        public void Initialize()
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

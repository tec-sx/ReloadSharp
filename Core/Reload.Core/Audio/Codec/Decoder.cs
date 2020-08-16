namespace Reload.Audio.Codec
{
    using Reload.Audio.Backend;
    using System;

    internal abstract class Decoder
    {
        protected AudioFormat audioFormat;
        protected int totalSamples = 0;

        public AudioFormat Format => audioFormat;
        public abstract TimeSpan Duration { get; }
        public abstract bool IsFinished { get; }

        protected abstract byte[] ReadSamples(int numberOfSamples);

        public byte[] ReadSamples(TimeSpan span) =>
            ReadSamples(span.Seconds * audioFormat.SampleRate * audioFormat.Channels);

        public byte[] ReadAllSamples() => ReadSamples(totalSamples * audioFormat.Channels);
    }
}

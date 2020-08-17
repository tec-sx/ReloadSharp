using Reload.Core.Audio;
using System;

namespace Reload.Platform.Audio.OpenAl.Codec
{
    internal abstract class Decoder
    {
        protected AudioFormat AudioFormat { get; set; }

        protected int totalSamples = 0;

        public AudioFormat Format => AudioFormat;
        public abstract TimeSpan Duration { get; }
        public abstract bool IsFinished { get; }

        protected abstract byte[] ReadSamples(int numberOfSamples);

        public byte[] ReadSamples(TimeSpan span) =>
            ReadSamples(span.Seconds * AudioFormat.SampleRate * AudioFormat.Channels);

        public byte[] ReadAllSamples() => ReadSamples(totalSamples * AudioFormat.Channels);
    }
}

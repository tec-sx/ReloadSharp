using NLayer;
using Reload.Core.Audio;
using System;
using System.IO;

namespace Reload.Platform.Audio.OpenAl.Codec.Mp3
{
    internal class Mp3Decoder : Decoder
    {
        private readonly MpegFile _mp3Stream;

        public override bool IsFinished => _mp3Stream.Position == _mp3Stream.Length;
        public override TimeSpan Duration => _mp3Stream.Duration;

        public Mp3Decoder(Stream stream)
        {
            _mp3Stream = new MpegFile(stream);

            AudioFormat = new AudioFormat
            {
                Channels = _mp3Stream.Channels,
                BitsPerSample = 16,
                SampleRate = _mp3Stream.SampleRate
            };

            totalSamples = (int)_mp3Stream.Length / sizeof(float);
        }

        protected override byte[] ReadSamples(int numberOfSamples)
        {
            int bytes = AudioFormat.BytesPerSample * numberOfSamples;
            var data = new byte[bytes];

            _mp3Stream.ReadSamplesInt16(data, 0, AudioFormat.Channels * bytes);

            return data;
        }
    }
}

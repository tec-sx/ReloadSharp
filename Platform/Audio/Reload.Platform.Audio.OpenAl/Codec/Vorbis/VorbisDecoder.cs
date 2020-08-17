using NVorbis;
using Reload.Core.Audio;
using System;
using System.IO;

namespace Reload.Platform.Audio.OpenAl.Codec.Vorbis
{
    internal class VorbisDecoder : Decoder
    {
        private readonly VorbisReader _reader;

        public override bool IsFinished => _reader.IsEndOfStream;
        public override TimeSpan Duration => _reader.TotalTime;

        public VorbisDecoder(Stream stream)
        {
            _reader = new VorbisReader(stream, true);

            AudioFormat = new AudioFormat
            {
                Channels = _reader.Channels,
                BitsPerSample = 16,
                SampleRate = _reader.SampleRate
            };

            totalSamples = (int)_reader.TotalSamples;
        }

        protected override byte[] ReadSamples(int numberOfSamples)
        {
            var bytes = AudioFormat.BytesPerSample * numberOfSamples;
            var readBuffer = new float[numberOfSamples];

            _reader.ReadSamples(readBuffer, 0, numberOfSamples);

            return CastBuffer(readBuffer, bytes, numberOfSamples);
        }

        private static byte[] CastBuffer(float[] inBuffer, int bytes, int length)
        {
            var outBuffer = new byte[bytes];

            for (int i = 0; i < length; i++)
            {
                var temp = (int)(short.MaxValue * inBuffer[i]);

                if (temp > short.MaxValue)
                {
                    temp = short.MaxValue;
                }
                else if (temp < short.MinValue)
                {
                    temp = short.MinValue;
                }

                outBuffer[2 * i] = (byte)(((short)temp) & 0xFF);
                outBuffer[2 * i + 1] = (byte)(((short)temp) >> 8);
            }

            return outBuffer;
        }
    }
}

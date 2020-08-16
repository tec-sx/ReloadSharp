namespace Reload.Audio.Codec.Mp3
{
    using NLayer;
    using System;
    using System.IO;

    internal class Mp3Decoder : Decoder
    {
        private readonly MpegFile _mp3Stream;

        public override bool IsFinished => _mp3Stream.Position == _mp3Stream.Length;
        public override TimeSpan Duration => _mp3Stream.Duration;

        public Mp3Decoder(Stream stream)
        {
            _mp3Stream = new MpegFile(stream);


            audioFormat.Channels = _mp3Stream.Channels;
            audioFormat.BitsPerSample = 16;
            audioFormat.SampleRate = _mp3Stream.SampleRate;

            totalSamples = (int)_mp3Stream.Length / sizeof(float);
        }

        protected override byte[] ReadSamples(int numberOfSamples)
        {
            int bytes = audioFormat.BytesPerSample * numberOfSamples;
            var data = new byte[bytes];

            _mp3Stream.ReadSamplesInt16(data, 0, audioFormat.Channels * bytes);

            return data;
        }
    }
}

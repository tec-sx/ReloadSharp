namespace Reload.Audio.Backend
{
    public struct AudioFormat
    {
        public int SampleRate;
        public int Channels;
        public int BitsPerSample;
        public int BytesPerSample => BitsPerSample / 8;
        public int BytesPerSecond => BytesPerSample * SampleRate * Channels;
    }
}

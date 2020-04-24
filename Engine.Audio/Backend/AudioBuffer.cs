namespace Engine.Audio.Backend
{
    using Silk.NET.OpenAL;
    using System;

    public class AudioBuffer : IDisposable
    {
        public AudioFormat Format { get; protected set; }

        public uint Buffer { get; }

        public AudioBuffer()
        {
            Buffer = ALNative.GenerateBuffer();
        }

        public void Dispose()
        {
            ALNative.DeleteBuffer(Buffer);
        }

        public void BufferData<T>(T[] data, AudioFormat audioFormat)
            where T : unmanaged
        {
            var bufferFormat = audioFormat.Channels == 2 ? BufferFormat.Stereo8 : BufferFormat.Mono8;

            if (audioFormat.BitsPerSample == 16)
            {
                bufferFormat++;
            }

            ALNative.BufferData(Buffer, bufferFormat, data, audioFormat.SampleRate);
            Format = audioFormat;
        }
    }
}

using Reload.Core.Audio;
using Reload.Core.Audio.Buffers;
using Silk.NET.OpenAL;

namespace Reload.Platform.Audio.OpenAl
{
    /// <summary>
    /// The openAL implementation of audio buffer.
    /// </summary>
    public sealed class OpenALBuffer : AudioBuffer
    {
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenALBuffer"/> class.
        /// </summary>
        public OpenALBuffer()
        {
            Buffer = OpenAl.GenerateBuffer();
        }

        /// <inheritdoc/>
        public override void SetData<T>(T[] data, AudioFormat audioFormat)
        {
            var bufferFormat = audioFormat.Channels == 2 ? BufferFormat.Stereo8 : BufferFormat.Mono8;

            if (audioFormat.BitsPerSample == 16)
            {
                bufferFormat++;
            }

            OpenAl.BufferData(Buffer, bufferFormat, data, audioFormat.SampleRate);
            Format = audioFormat;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            { }

            OpenAl.DeleteBuffer(Buffer);

            _isDisposed = true;
        }
    }
}

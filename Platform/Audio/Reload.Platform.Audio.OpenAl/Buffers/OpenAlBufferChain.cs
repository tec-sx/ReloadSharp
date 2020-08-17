using Silk.NET.OpenAL;
using System.Collections.Generic;
using Reload.Core.Audio.Buffers;
using Reload.Core.Audio;

namespace Reload.Platform.Audio.OpenAl.Buffers
{
    /// <summary>
    /// an OpenAl implementation of an audio buffer chain.
    /// </summary>
    public sealed class OpenAlBufferChain : BufferChain
    {
        private readonly uint _source;

        private readonly List<AudioBuffer> _buffers;
        
        private readonly int _numBuffers = 3;
        
        private int _currentBuffer;

        private bool _isDisposed;

        /// <inheritdoc/>
        public override int BuffersQueued
        {
            get
            {
                RemoveProcessed();
                return OpenAl.GetSourceProperty(_source, GetSourceInteger.BuffersQueued);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAlBufferChain"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public OpenAlBufferChain(uint source)
        {
            _source = source;
            _buffers = new List<AudioBuffer>();

            for (var i = 0; i < _numBuffers; i++)
            {
                _buffers.Add(new OpenAlBuffer());
            }
        }


        /// <inheritdoc/>
        public override void QueueData<T>(T[] data, AudioFormat format)
        {
            RemoveProcessed();

            var buffer = _buffers[_currentBuffer].Buffer;

            _buffers[_currentBuffer].SetData(data, format);
            _currentBuffer++;
            _currentBuffer %= 3;

            OpenAl.SourceQueueBuffers(_source, new[] { buffer });
        }

        /// <inheritdoc/>
        protected override void RemoveProcessed()
        {
            var processed = OpenAl.GetSourceProperty(_source, GetSourceInteger.BuffersProcessed);

            while (processed > 0)
            {
                OpenAl.SourceUnqueueBuffers(_source, new uint[] { 1 });
                processed--;
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _buffers?.ForEach(buffer => buffer.Dispose());
                _buffers?.Clear();
            }

            _isDisposed = true;
        }
    }
}

using Reload.Audio.Backend;

namespace Reload.Audio
{
    using Reload.Audio.Backend;
    using Silk.NET.OpenAL;
    using System;
    using System.Collections.Generic;

    internal sealed class BufferChain : IDisposable
    {
        private readonly uint _source;
        private readonly List<AudioBuffer> _buffers;

        private readonly int _numBuffers = 3;
        private int _currentBuffer = 0;

        public int BuffersQueued
        {
            get
            {
                RemoveProcessed();
                return ALNative.GetSourceProperty(_source, GetSourceInteger.BuffersQueued);
            }
        }

        public BufferChain(uint source)
        {
            _source = source;
            _buffers = new List<AudioBuffer>();

            for (int i = 0; i < _numBuffers; i++)
            {
                _buffers.Add(new AudioBuffer());
            }
        }

        public void Dispose()
        {
            _buffers?.ForEach(buffer => buffer.Dispose());
            _buffers?.Clear();
        }

        public void QueueData<T>(T[] data, AudioFormat format) where T : unmanaged
        {
            RemoveProcessed();

            var buffer = _buffers[_currentBuffer].Buffer;

            _buffers[_currentBuffer].BufferData(data, format);
            _currentBuffer++;
            _currentBuffer %= 3;

            ALNative.SourceQueueBuffers(_source, new uint[] { buffer });
        }

        public void RemoveProcessed()
        {
            var processed = ALNative.GetSourceProperty(_source, GetSourceInteger.BuffersProcessed);

            while (processed > 0)
            {
                ALNative.SourceUnqueueBuffers(_source, new uint[] { 1 });
                processed--;
            }
        }
    }
}

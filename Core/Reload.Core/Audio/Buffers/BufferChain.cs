using System;

namespace Reload.Core.Audio.Buffers
{
    /// <summary>
    /// An audio buffer chain.
    /// </summary>
    public abstract class BufferChain : IDisposable
    {
        /// <summary>
        /// Gets the number of buffers that are currently queued.
        /// </summary>
        public abstract int BuffersQueued { get; }

        /// <summary>
        /// Queues data in the current buffer chain.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="format">The format.</param>
        public abstract void QueueData<T>(T[] data, AudioFormat format) where T : unmanaged;

        /// <summary>
        /// Removes the processed.
        /// </summary>
        protected abstract void RemoveProcessed();

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

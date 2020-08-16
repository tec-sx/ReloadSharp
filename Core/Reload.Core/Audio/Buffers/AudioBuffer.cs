using System;

namespace Reload.Core.Audio.Buffers
{
    /// <summary>
    /// The audio buffer.
    /// </summary>
    public abstract class AudioBuffer : IDisposable
    {
        /// <summary>
        /// Gets or sets the audio format.
        /// </summary>
        public AudioFormat Format { get; protected set; }

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public uint Buffer { get; protected set; }

        /// <summary>
        /// Sets the data to the buffer.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="audioFormat">The audio format.</param>
        public abstract void SetData<T>(T[] data, AudioFormat audioFormat) where T : unmanaged;

        /// <summary>
        /// Creates a new audio buffer.
        /// </summary>
        /// <returns>An AudioBuffer.</returns>
        public static AudioBuffer Create() => AudioFactory.Create().AudioBuffer();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);
    }
}

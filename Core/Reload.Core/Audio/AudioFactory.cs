using System.IO;

namespace Reload.Core.Audio.Buffers
{
    /// <summary>
    /// The audio factory implementation.
    /// </summary>
    public abstract class AudioFactory
    {
        /// <summary>
        /// Creates new audio buffer.
        /// </summary>
        /// <returns>An AudioBuffer.</returns>
        protected internal abstract AudioBuffer CreateAudioBuffer();

        /// <summary>
        /// Creates new audio source from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>An AudioSource.</returns>
        protected internal abstract AudioSource CreateAudioSource(Stream stream);
    }
}

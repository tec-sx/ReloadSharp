using System.IO;

namespace Reload.Core.Audio.Buffers
{
    /// <summary>
    /// The audio factory implementation.
    /// </summary>
    public abstract class AudioFactoryImplementation
    {
        /// <summary>
        /// Creates new audio buffer.
        /// </summary>
        /// <returns>An AudioBuffer.</returns>
        public abstract AudioBuffer AudioBuffer();

        /// <summary>
        /// Creates new audio source from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>An AudioSource.</returns>
        public abstract AudioSource AudioSource(Stream stream);
    }
}

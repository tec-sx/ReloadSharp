using Reload.Core.Audio;

namespace Reload.Platform.Audio.OpenAl
{
    /// <summary>
    /// The OpenAL audio backend.
    /// </summary>
    public class OpenAlBackend : IAudioBackend
    {
        /// <inheritdoc/>
        public AudioBackendType Type => AudioBackendType.OpenAL;

        /// <inheritdoc/>
        public void Initialize()
        {
        }

        /// <inheritdoc/>
        public void ShutDown()
        {
        }
    }
}

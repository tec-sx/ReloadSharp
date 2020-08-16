using Reload.Core.Game;

namespace Reload.Core.Audio
{
    public interface IAudioBackend : ISubSystem
    {
        /// <summary>
        /// Gets the audio backend type.
        /// </summary>
        AudioBackendType Type { get; }
    }
}

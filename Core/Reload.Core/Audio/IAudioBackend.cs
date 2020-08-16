using Reload.Core.Game;

namespace Reload.Core.Audio
{
    public interface IAudioBackend : ISubSystem
    {
        /// <summary>
        /// Gets the audio backend type.
        /// </summary>
        AudioBackendType Type { get; init; }

        bool IsExtensionPresent(string ext);

        #region Generators

        uint GenerateBuffer();

        void DeleteBuffer(uint buffer);

        void BufferData<T>(uint buffer, BufferFormat bufferFormat, T[] data, int sampleRate)
            where T : unmanaged;

        uint GenerateSource();

        void DeleteSource(uint source);
        #endregion
    }
}

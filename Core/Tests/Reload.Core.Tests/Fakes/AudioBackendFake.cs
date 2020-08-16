using Reload.Core.Audio;

namespace Reload.Core.Tests.Fakes
{
    internal class AudioBackendFake : IAudioBackend
    {
        public AudioBackendType Type => AudioBackendType.None;

        public void BufferData<T>(uint buffer, AudioBufferFormat bufferFormat, T[] data, int sampleRate) where T : unmanaged
        { }

        public void DeleteBuffer(uint buffer)
        { }

        public void DeleteSource(uint source)
        { }

        public uint GenerateBuffer()
        {
            return 0;
        }

        public uint GenerateSource()
        {
            return 0;
        }

        public void Initialize()
        { }

        public bool IsExtensionPresent(string ext)
        {
            return false;
        }

        public void ShutDown()
        { }
    }
}

using Reload.Core.Audio;
using Reload.Core.Audio.Buffers;

namespace Reload.Core.Tests.Fakes
{
    internal class AudioAPIFake : AudioAPI
    {
        public AudioAPIType Type => AudioAPIType.None;

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

        public override void StartUp()
        { }

        public bool IsExtensionPresent(string ext)
        {
            return false;
        }

        public override void ShutDown()
        { }
    }
}

using Core.CoreSystem.Audio.Device;

namespace Core.CoreSystem.Audio
{
    internal sealed class AudioManager : IAudioManager
    {
        public IAudioDevice Device { get; private set; }
        
        public void CreateDevice()
        {
            Device = new AudioDevice();
        }

        public void DisposeResources()
        {
            Device.Dispose();
        }
    }
}
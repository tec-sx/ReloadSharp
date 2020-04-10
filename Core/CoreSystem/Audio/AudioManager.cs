namespace Core.CoreSystem.Audio
{
    using Core.CoreSystem.Audio.Device;
    
    internal sealed class AudioManager
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
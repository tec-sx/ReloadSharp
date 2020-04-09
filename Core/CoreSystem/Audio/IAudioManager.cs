using Core.CoreSystem.Audio.Device;

namespace Core.CoreSystem.Audio
{
    internal interface IAudioManager
    {
        IAudioDevice Device { get; }
        
        void CreateDevice();
        void DisposeResources();
    }
}
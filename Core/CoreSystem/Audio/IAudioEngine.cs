namespace Core.CoreSystem.Audio
{
    using System;

    public interface IAudioEngine: IDisposable
    {
        void Init();
    }
}
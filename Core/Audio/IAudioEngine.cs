namespace Core.Audio
{
    using System;

    public interface IAudioEngine: IDisposable
    {
        void Init();
    }
}
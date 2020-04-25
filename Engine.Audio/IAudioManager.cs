namespace Engine.Audio
{
    using System;

    public interface IAudioManager : IDisposable
    {
        void CreateContext();
    }
}

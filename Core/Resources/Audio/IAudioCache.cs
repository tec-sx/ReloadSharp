using System;

namespace Core.Resources.Audio
{
    public interface IAudioCache : IDisposable
    {
        IMusic LoadMusic(string fullPath);
        ISound LoadSound(string fullPath);
    }
}

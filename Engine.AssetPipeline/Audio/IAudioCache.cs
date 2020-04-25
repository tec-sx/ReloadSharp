
namespace Engine.AssetPipeline.Audio
{
    using System;
    using Models;

    public interface IAudioCache : IDisposable
    {
        IMusic LoadMusic(string fullPath);
        ISound LoadSound(string fullPath);
    }
}


namespace Engine.AssetPipeline.Audio
{
    using System;
    using Models;

    public interface IAudioCache
    {
        IMusic LoadMusic(string fullPath);
        ISound LoadSound(string fullPath);
        void CleanUp();
    }
}

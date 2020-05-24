
namespace Reload.AssetPipeline.Audio
{
    using Models;

    public interface IAudioCache
    {
        IMusic LoadMusic(string fullPath);
        ISound LoadSound(string fullPath);
        void CleanUp();
    }
}

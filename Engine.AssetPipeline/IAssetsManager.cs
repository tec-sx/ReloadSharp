
namespace Engine.AssetPipeline
{
    using Audio.Models;
    using GameObjects.Models;
    using Textures.Models;

    public interface IAssetsManager
    {
        void Initialize(AssetsConfiguration assetsConfiguration);
        ITexture GetTexture(string file);
        IGameObject GetGameObject(string file);
        IMusic LoadMusic(string file);
        ISound LoadSound(string file);
        void ShutDown();
    }
}
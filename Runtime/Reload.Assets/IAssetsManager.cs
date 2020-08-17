namespace Reload.Assets
{
    using GameObjects.Models;
    using Reload.Core.Audio;
    using Textures.Models;

    public interface IAssetsManager
    {
        void Initialize(AssetsConfiguration assetsConfiguration);
        ITexture GetTexture(string file);
        IGameObject GetGameObject(string file);
        Music LoadMusic(string file);
        Sound LoadSound(string file);
        void ShutDown();
    }
}
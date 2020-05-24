using Reload.AssetPipeline.Audio;
using Reload.AssetPipeline.Audio.Models;
using Reload.AssetPipeline.GameObjects;
using Reload.AssetPipeline.GameObjects.Models;
using Reload.AssetPipeline.Textures;
using Reload.AssetPipeline.Textures.Models;

namespace Reload.AssetPipeline
{
    using Reload.AssetPipeline.Audio;
    using Reload.AssetPipeline.Audio.Models;
    using Reload.AssetPipeline.GameObjects;
    using Reload.AssetPipeline.GameObjects.Models;
    using Reload.AssetPipeline.Textures;
    using Reload.AssetPipeline.Textures.Models;
    using System.IO;

    public class AssetsManager : IAssetsManager
    {
        private AssetsConfiguration _assetsConfiguration;

        private readonly ITextureCache _textureCache;
        private readonly IGameObjectCache _gameObjectCache;
        private readonly IAudioCache _audioCache;

        public AssetsManager(
            ITextureCache textureCache,
            IGameObjectCache gameObjectCache,
            IAudioCache audioCache
            )
        {
            _textureCache = textureCache;
            _gameObjectCache = gameObjectCache;
            _audioCache = audioCache;
        }

        public void Initialize(AssetsConfiguration assetsConfiguration)
        {
            _assetsConfiguration = assetsConfiguration;
        }

        public void ShutDown()
        {
            _textureCache.CleanUp();
            _gameObjectCache.CleanUp();
        }

        public ITexture GetTexture(string file)
        {
            var fullPath = Path.Combine(_assetsConfiguration.TexturesPath, $"{file}.{_assetsConfiguration.TextureFormat}");
            return _textureCache.GetTexture(fullPath);
        }

        public IGameObject GetGameObject(string file)
        {
            var fullPath = Path.Combine(_assetsConfiguration.ModelsPath, $"{file}.{_assetsConfiguration.ModelFormat}");
            return _gameObjectCache.GetGameObject(fullPath);
        }

        public IMusic LoadMusic(string file)
        {
            string fullPath = Path.Combine(_assetsConfiguration.MusicPath, $"{file}.{_assetsConfiguration.SoundFormat}");
            return _audioCache.LoadMusic(fullPath);
        }

        public ISound LoadSound(string file)
        {
            var fullPath = Path.Combine(_assetsConfiguration.SoundsPath, $"{file}.{_assetsConfiguration.SoundFormat}");
            return _audioCache.LoadSound(fullPath);
        }
    }
}
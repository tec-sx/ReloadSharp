namespace Core.Resources
{
    using System.IO;
    using Config;
    using Textures;
    using GameObjects;
    using GameObjects.Models;
    using Audio;

    public class AssetsManager : IAssetsManager
    {
        private readonly ApplicationSettings _settings;
        private readonly ContentPath _contentPath;

        private readonly ITextureCache _textureCache;
        private readonly IGameObjectCache _gameObjectCache;
        private readonly IAudioCache _audioCache;

        public AssetsManager(
            ITextureCache textureCache,
            IGameObjectCache gameObjectCache,
            IAudioCache audioCache)
        {
            _settings = Configuration.Settings;
            _contentPath = Configuration.ContentPath;

            _textureCache = textureCache;
            _gameObjectCache = gameObjectCache;
            _audioCache = audioCache;

        }

        public void Dispose()
        {
            _textureCache.Dispose();
            _gameObjectCache.Dispose();
        }

        public ITexture GetTexture(string file)
        {
            var fullPath = Path.Combine(_contentPath.Textures, $"{file}.{_settings.Image.Format}");
            return _textureCache.GetTexture(fullPath);
        }

        public IGameObject GetGameObject(string file)
        {
            var fullPath = Path.Combine(_contentPath.Models, $"{file}.{_settings.Model.Format}");
            return _gameObjectCache.GetGameObject(fullPath);
        }

        public IMusic LoadMusic(string file)
        {
            string fullPath = Path.Combine(_contentPath.Music, $"{file}.{_settings.Audio.Format}");
            return _audioCache.LoadMusic(fullPath);
        }

        public ISound LoadSound(string file)
        {
            var fullPath = Path.Combine(_contentPath.Sounds, $"{file}.{_settings.Audio.Format}");
            return _audioCache.LoadSound(fullPath);
        }
    }
}
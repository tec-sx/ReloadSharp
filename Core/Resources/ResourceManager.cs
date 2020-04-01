namespace Core.Resources
{
    using System.IO;
    using Config;
    using Textures;
    using GameObjects;
    using Audio;

    public class ResourceManager : IResourceManager
    {
        private readonly ApplicationSettings _settings;
        private readonly ContentPath _contentPath;

        private readonly ITextureCache _textureCache;
        private readonly IGameObjectCache _gameObjectCache;
        private readonly IAudioCache _audioCache;

        public ResourceManager(
            IConfiguration configuration,
            ITextureCache textureCache,
            IGameObjectCache gameObjectCache,
            IAudioCache audioCache)
        {
            _settings = configuration.Settings;
            _contentPath = configuration.ContentPath;

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
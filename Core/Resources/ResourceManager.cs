namespace Core.Resources
{
    using System.IO;
    using Config;
    using Texture;
    using Raylib_cs;
    
    public class ResourceManager : IResourceManager
    {
        private readonly ApplicationSettings _settings;
        private readonly ContentPath _contentPath;
        private readonly TextureCache _textureCache;
        
        public ResourceManager(IConfiguration configuration)
        {
            _settings = configuration.Settings;
            _contentPath = configuration.ContentPath;
            _textureCache = new TextureCache();
        }
        
        public Texture2D GetTexture(string file)
        {
            var fullPath = Path.Combine(_contentPath.Textures, $"{file}.{_settings.Image.Format}");
            return _textureCache.GetTexture(fullPath);
        }

        public void Dispose()
        {
            _textureCache.Dispose();
        }
    }
}
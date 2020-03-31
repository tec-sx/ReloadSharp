namespace Core.Resources
{
    using System;
    using System.IO;
    using Texture;
    using Raylib_cs;
    
    public class ResourceManager : IResourceManager
    {
        private readonly TextureCache _textureCache;

        public ResourceManager()
        {
            _textureCache = new TextureCache();
        }
        
        public Texture2D GetTexture(string file)
        {
            return _textureCache.GetTexture(file);
        }

        public void Dispose()
        {
            _textureCache.Dispose();
        }
    }
}
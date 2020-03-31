namespace Core.Resources.Texture
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Raylib_cs;

    public class TextureCache : IDisposable
    {
        private readonly Dictionary<string, Texture2D> _textureDict;
        
        public TextureCache()
        {
            _textureDict = new Dictionary<string, Texture2D>();
        }
        
        public void Dispose()
        {
            foreach (var (key, value) in _textureDict)
            {
                Raylib.UnloadTexture(value);
                _textureDict.Remove(key);
            }
        }
        
        public Texture2D GetTexture(string fullPath)
        {
            if (_textureDict.TryGetValue(fullPath, out var texture))
            {
                return texture;
            }
            
            var image = Raylib.LoadImage(fullPath);
            Raylib.ImageFormat(ref image, (int)PixelFormat.UNCOMPRESSED_R8G8B8A8);

            texture = Raylib.LoadTextureFromImage(image);
            _textureDict.Add(fullPath, texture);
            
            // Unload image
            
            return texture;
        }
    }
}
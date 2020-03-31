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
        
        public Texture2D GetTexture(string file)
        {
            string fullPath = Path.Combine($"{Environment.CurrentDirectory}", $"Assets/Textures/{file}.png");

            if (_textureDict.TryGetValue(fullPath, out var texture))
            {
                return texture;
            }
            
            texture = Raylib.LoadTexture(fullPath);
            _textureDict.Add(fullPath, texture);

            return texture;
        }
    }
}
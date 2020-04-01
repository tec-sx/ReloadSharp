namespace Core.Resources.Textures
{
    using System;
    using System.Collections.Generic;
    using Raylib_cs;

    public class TextureCache_RL : ITextureCache
    {
        private readonly Dictionary<string, Texture2D> _textureDict;

        public TextureCache_RL()
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

        public ITexture GetTexture(string fullPath)
        {
            if (!_textureDict.TryGetValue(fullPath, out var texture))
            {
                var image = Raylib.LoadImage(fullPath);
                Raylib.ImageFormat(ref image, (int)PixelFormat.UNCOMPRESSED_R8G8B8A8);

                texture = Raylib.LoadTextureFromImage(image);
                _textureDict.Add(fullPath, texture);

                Raylib.UnloadImage(image);
            }

            return new Texture_RL(texture);
        }
    }
}
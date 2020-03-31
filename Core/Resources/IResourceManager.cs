using System;

namespace Core.Resources
{
    using Raylib_cs;
    
    public interface IResourceManager : IDisposable
    {
        Texture2D GetTexture(string file);
    }
}
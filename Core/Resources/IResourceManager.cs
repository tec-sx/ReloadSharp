namespace Core.Resources
{
    using System;
    using Raylib_cs;
    
    public interface IResourceManager : IDisposable
    {
        Texture2D GetTexture(string file);
    }
}
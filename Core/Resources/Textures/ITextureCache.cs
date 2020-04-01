namespace Core.Resources.Textures
{
    using System;

    public interface ITextureCache : IDisposable
    {
        ITexture GetTexture(string fullPath);
    }
}

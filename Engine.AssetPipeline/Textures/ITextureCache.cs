namespace Core.AssetsPipeline.Textures
{
    using System;
    using Models;

    public interface ITextureCache : IDisposable
    {
        ITexture GetTexture(string fullPath);
    }
}

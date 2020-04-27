namespace Engine.AssetPipeline.Textures
{
    using System;
    using Models;

    public interface ITextureCache
    {
        ITexture GetTexture(string fullPath);
        void CleanUp();
    }
}

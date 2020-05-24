namespace Reload.AssetPipeline.Textures
{
    using Models;

    public interface ITextureCache
    {
        ITexture GetTexture(string fullPath);
        void CleanUp();
    }
}

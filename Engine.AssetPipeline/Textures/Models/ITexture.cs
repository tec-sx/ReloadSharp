namespace Reload.AssetPipeline.Textures.Models
{
    public interface ITexture
    {
        void Update();
        void Render(int x, int y, int w, int h);
    }
}

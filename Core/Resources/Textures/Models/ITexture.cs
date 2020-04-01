namespace Core.Resources.Textures
{
    public interface ITexture
    {
        void Update();
        void Render(int x, int y, int w, int h);
    }
}

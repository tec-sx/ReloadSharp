namespace Reload.Rendering
{
    public delegate Texture2D CreateBlankTextureDelegate(uint width, uint height);
    public delegate Texture2D CreateTextureFromFileDelegate(string path);

    public abstract class Texture2D : Texture
    {
        public static CreateBlankTextureDelegate CreateBlank;
        public static CreateTextureFromFileDelegate CreateFromFile;
    }
}

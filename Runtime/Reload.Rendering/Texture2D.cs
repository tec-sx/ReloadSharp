namespace Reload.Rendering
{
    public delegate Texture2D DCreateBlankTexture2D(uint width, uint height);
    public delegate Texture2D DCreateTexture2DFromFile(string path);

    public abstract class Texture2D : Texture
    {
        public static DCreateBlankTexture2D CreateBlank;
        public static DCreateTexture2DFromFile CreateFromFile;
    }
}

namespace Reload.Rendering
{
    public delegate TextureCube DCreateBlankTextureCube(TextureFormat format, uint width, uint height);

    public delegate TextureCube DCreateTextureCubeFromFile(string path);
    
    public abstract class TextureCube : Texture
    {
        public static DCreateBlankTextureCube CreateBlank;
        public static DCreateTextureCubeFromFile CreateFromFile;
        public abstract string GetPath();
    }
}
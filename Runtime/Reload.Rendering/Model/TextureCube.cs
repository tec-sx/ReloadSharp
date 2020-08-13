namespace Reload.Resources.Model
{
    public delegate TextureCube DCreateBlankTextureCube(TextureFormat format, uint width, uint height);

    public delegate TextureCube DCreateTextureCubeFromFile(string path);

    /// <summary>
    /// The texture cube.
    /// </summary>
    public abstract class TextureCube : Texture
    {
        /// <summary>
        /// Creates a cube white 2D texture.
        /// </summary>
        public static DCreateBlankTextureCube CreateBlank { get; set; }

        /// <summary>
        /// Create a cube textrure from image file. 
        /// </summary>
        public static DCreateTextureCubeFromFile CreateFromFile { get; set; }
        
        public abstract string GetPath();
    }
}
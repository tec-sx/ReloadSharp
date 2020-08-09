using System;

namespace Reload.Rendering
{
    public delegate Texture2D DCreateBlankTexture2D(uint width, uint height);

    public delegate Texture2D DCreateTexture2DFromFile(string path);

    /// <summary>
    /// A class for wirking with 2D (image, color, etc.) textures.
    /// </summary>
    public abstract class Texture2D : Texture
    {
        /// <summary>
        /// Gets or sets the writeable texture buffer.
        /// </summary>
        public Memory<byte> Buffer { get; protected set; }

        /// <summary>
        /// Creates a blank white 2D texture.
        /// </summary>
        public static DCreateBlankTexture2D CreateBlank { get; set; }

        /// <summary>
        /// Create a 2D textrure from image file. 
        /// </summary>
        public static DCreateTexture2DFromFile CreateFromFile { get; set; }

        public abstract void IsLoaded();

        public abstract void Lock();

        public abstract void Unlock();

        public abstract void Resize(uint width, uint height);

        public abstract string GetPath();
    }
}

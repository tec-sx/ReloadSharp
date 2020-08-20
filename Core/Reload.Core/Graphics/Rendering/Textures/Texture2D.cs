using Reload.Core.Exceptions;
using System;

namespace Reload.Core.Graphics.Rendering.Textures
{


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
        /// Is The texture loaded.
        /// </summary>
        public abstract void IsLoaded();

        /// <summary>
        /// Locks the texture.
        /// </summary>
        public abstract void Lock();

        /// <summary>
        /// Unlocks the texture.
        /// </summary>
        public abstract void Unlock();

        /// <summary>
        /// Resizes the texture.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public abstract void Resize(uint width, uint height);

        /// <summary>
        /// Gets the texture path.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string GetPath();

        /// <summary>
        /// Creates a blank white 2D texture.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A Texture2D.</returns>
        public static Texture2D CreateBlank(uint width, uint height)
        {
            return GraphicsAPI.TextureFactory.CreateBlankTexture2D(width, height)
                ?? throw new ReloadFactoryNotImplementedException(typeof(TextureFactory).ToString());
        }

        /// <summary>
        /// Creates a 2D texture from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A Texture2D.</returns>
        public static Texture2D CreateFromFile(string path)
        {
            return GraphicsAPI.TextureFactory.CreateTexture2DFromFile(path)
                ?? throw new ReloadFactoryNotImplementedException(typeof(TextureFactory).ToString());
        }
    }
}

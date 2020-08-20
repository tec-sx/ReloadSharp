namespace Reload.Core.Graphics.Rendering.Textures
{
    /// <summary>
    /// The texture factory implementation.
    /// </summary>
    public abstract class TextureFactory
    {
        /// <summary>
        /// Creates a blank 2D texture.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A Texture2D.</returns>
        protected internal abstract Texture2D CreateBlankTexture2D(uint width, uint height);

        /// <summary>
        /// Creates a 2D texture from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A Texture2D.</returns>
        protected internal abstract Texture2D CreateTexture2DFromFile(string path);

        /// <summary>
        /// Creates a blank texture cube.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A TextureCube.</returns>
        protected internal abstract TextureCube CreateBlankTextureCube(TextureFormat format, uint width, uint height);

        /// <summary>
        /// Creates a texture cube from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A TextureCube.</returns>
        protected internal abstract TextureCube CreateTextureCubeFromFile(string path);
    }
}

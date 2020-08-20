using Reload.Core.Exceptions;

namespace Reload.Core.Graphics.Rendering.Textures
{
    /// <summary>
    /// The texture cube.
    /// </summary>
    public abstract class TextureCube : Texture
    {
        /// <summary>
        /// Creates a blank texture cube.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A TextureCube.</returns>
        public static TextureCube CreateBlank(TextureFormat format, uint width, uint height)
        {
            return GraphicsAPI.TextureFactory?.CreateBlankTextureCube(format, width, height)
                ?? throw new ReloadFactoryNotImplementedException(typeof(TextureFactory).ToString());
        }

        /// <summary>
        /// Creates a textrue cube from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A TextureCube.</returns>
        public static TextureCube CreateFromFile(string path)
        {
            return GraphicsAPI.TextureFactory?.CreateTextureCubeFromFile(path)
                ?? throw new ReloadFactoryNotImplementedException(typeof(TextureFactory).ToString());
        }

        /// <summary>
        /// Gets the texture path.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string GetPath();
    }
}
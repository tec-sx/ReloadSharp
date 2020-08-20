namespace Reload.Core.Graphics.Rendering.Textures
{
    using System;

    public enum TextureFormat
    {
        None = 0,
        Rgb,
        Rgba,
        Float16
    }

    public enum TextureWrap
    {
        None = 0,
        Clamp,
        Repeat
    }

    /// <summary>
    /// The texture base class.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        uint Width { get; }

        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        uint Height { get; }

        /// <summary>
        /// Uploads given texture data to the gpu.
        /// </summary>
        /// <param name="data">The data.</param>
        void SetData(Span<byte> data);

        /// <summary>
        /// Binds the texture to a texture target.
        /// Default slot is 0.
        /// </summary>
        /// <param name="slot">The texture slot.</param>
        void Bind(uint slot = 0);
    }
}

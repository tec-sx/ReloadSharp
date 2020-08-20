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
    public abstract class Texture : IDisposable
    {
        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        public uint Width { get; init; }

        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        public uint Height { get; init; }

        /// <summary>
        /// Uploads given texture data to the gpu.
        /// </summary>
        /// <param name="data">The data.</param>
        public abstract void SetData(Span<byte> data);

        /// <summary>
        /// Binds the texture to a texture target.
        /// Default slot is 0.
        /// </summary>
        /// <param name="slot">The texture slot.</param>
        public abstract void Bind(uint slot = 0);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);
    }
}

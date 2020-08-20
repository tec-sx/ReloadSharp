using Reload.Core.Common;
using Reload.Core.Graphics.Rendering.Textures.NullTextures;
using System;

namespace Reload.Core.Graphics.Rendering.Textures
{
    /// <summary>
    /// The texture cube.
    /// </summary>
    public abstract class TextureCube : ITexture, IBindable, IDisposable
    {
        /// <inheritdoc/>
        public uint Width { get; protected set; }

        /// <inheritdoc/>
        public uint Height { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureCube"/> class.
        /// </summary>
        protected TextureCube()
        { }

        /// <inheritdoc/>
        public abstract void SetData(Span<byte> data);

        /// <inheritdoc/>
        public abstract void Bind(uint slot);

        /// <summary>
        /// Binds the texture to a texture target.
        /// Default slot is 0.
        /// </summary>
        /// <param name="slot">The texture slot.</param>
        public void Bind() => Bind(0);

        /// <summary>
        /// Uninds the texture from a texture target.
        /// </summary>
        public abstract void Unbind();

        /// <summary>
        /// Creates a blank texture cube.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A TextureCube.</returns>
        public static TextureCube CreateBlank(TextureFormat format, uint width, uint height)
        {
            return GraphicsAPI.TextureFactory?.CreateBlankTextureCube(format, width, height) ?? new NullTextureCube();
        }

        /// <summary>
        /// Creates a textrue cube from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A TextureCube.</returns>
        public static TextureCube CreateFromFile(string path)
        {
            return GraphicsAPI.TextureFactory?.CreateTextureCubeFromFile(path) ?? new NullTextureCube();
        }

        /// <summary>
        /// Gets the texture path.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string GetPath();

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
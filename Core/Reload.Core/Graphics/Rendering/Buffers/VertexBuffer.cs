using Reload.Core.Common;
using Reload.Core.Exceptions;
using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    public enum VertexBufferUsage
    {
        None = 0,
        Static,
        Dynamic
    }

    /// <summary>
    /// The vertex buffer.
    /// </summary>
    public abstract class VertexBuffer : IBindable, IDisposable
    {
        /// <summary>
        /// Gets the vertex shader data input layout.
        /// </summary>
        public BufferLayout Layout { get; }

        /// <summary>
        /// Prevents a default instance of the <see cref="VertexBuffer"/> class from being created.
        /// </summary>
        private VertexBuffer()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBuffer"/> class
        /// with shader layout defined by the layout parameter.
        /// </summary>
        protected VertexBuffer(BufferLayout layout)
        {
            Layout = layout;
        }


        /// <summary>
        /// Factory method for creating a new vertex buffer with predefined data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>VertexBuffer filled with the data passed.</returns>
        public static VertexBuffer Create(
            Span<float> data,
            BufferLayout layout,
            VertexBufferUsage usage = VertexBufferUsage.Static)
        {
            return GraphicsAPI.BufferFactory?.CreateVertexBuffer(data, layout, usage)
                ?? throw new ReloadFactoryNotImplementedException(typeof(BufferFactory).ToString());
        }

        /// <summary>
        /// Factory method for creating a new empty vertex buffer.
        /// </summary>
        /// <param name="size">The buffer size.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>Empty VertexBuffer.</returns>
        public static VertexBuffer CreateEmpty(
            uint size,
            BufferLayout layout,
            VertexBufferUsage usage = VertexBufferUsage.Dynamic)
        {
            return GraphicsAPI.BufferFactory?.CreateVertexBuffer(size, layout, usage)
                ?? throw new ReloadFactoryNotImplementedException(typeof(BufferFactory).ToString());
        }

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

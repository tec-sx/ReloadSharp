using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// An graphic buffer factory used to abstract the creation of buffers
    /// from their implementation.
    /// </summary>
    public abstract class BufferFactory
    {
        /// <summary>
        /// Creates a new vertex buffer with predefined data.
        /// </summary>
        /// <param name="data">The buffer data.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>A VertexBuffer.</returns>
        protected internal abstract VertexBuffer CreateVertexBuffer(Span<float> data, BufferLayout layout, VertexBufferUsage usage);

        /// <summary>
        /// Creates a new empty vertex buffer.
        /// </summary>
        /// <param name="size">The buffer size.</param>
        /// <param name="layout">The buffer  layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>A VertexBuffer.</returns>
        protected internal abstract VertexBuffer CreateVertexBuffer(uint size, BufferLayout layout, VertexBufferUsage usage);

        /// <summary>
        /// Creates a new index buffer with predefined data.
        /// </summary>
        /// <param name="indices">The indices.</param>
        /// <returns>An IndexBuffer.</returns>
        protected internal abstract IndexBuffer CreateIndexBuffer(Span<uint> indices);

        /// <summary>
        /// Creates a new vertex array.
        /// </summary>
        /// <returns>A VertexArray.</returns>
        protected internal abstract VertexArray CreateVertexArray();

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Graphics buffers service.";
        }
    }
}

using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// An graphic buffer factory implementation contract used to abstract the creation of buffers
    /// from their implementation.
    /// </summary>
    public abstract class BufferFactoryImplementation
    {
        /// <summary>
        /// Creates a new vertex buffer with predefined data.
        /// </summary>
        /// <param name="data">The buffer data.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>A VertexBuffer.</returns>
        public abstract VertexBuffer VertexBuffer(Span<float> data, BufferLayout layout, VertexBufferUsage usage);

        /// <summary>
        /// Creates a new empty vertex buffer.
        /// </summary>
        /// <param name="size">The buffer size.</param>
        /// <param name="layout">The buffer  layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>A VertexBuffer.</returns>
        public abstract VertexBuffer VertexBuffer(uint size, BufferLayout layout, VertexBufferUsage usage);

        /// <summary>
        /// Creates a new index buffer with predefined data.
        /// </summary>
        /// <param name="indices">The indices.</param>
        /// <returns>An IndexBuffer.</returns>
        public abstract IndexBuffer IndexBuffer(Span<uint> indices);
    }
}

using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// The index buffer.
    /// </summary>
    public abstract class IndexBuffer : IBuffer, IDisposable
    {
        /// <summary>
        /// Gets or sets the indices count.
        /// </summary>
        public uint Count { get; protected set; }

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

        /// <summary>
        /// Factory method for creating a new index buffer.
        /// </summary>
        /// <param name="indices">The indices.</param>
        /// <returns>An IndexBuffer.</returns>
        public static IndexBuffer Create(Span<uint> indices)
        {
            return BufferFactory.Create().IndexBuffer(indices);
        }

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

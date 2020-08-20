using Reload.Core.Common;
using Reload.Core.Exceptions;
using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// The index buffer.
    /// </summary>
    public abstract class IndexBuffer : IBindable, IDisposable
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
            return GraphicsAPI.BufferFactory?.CreateIndexBuffer(indices) 
                ?? throw new ReloadFactoryNotImplementedException(typeof(BufferFactory).ToString());
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

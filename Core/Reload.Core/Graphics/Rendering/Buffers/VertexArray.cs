using Reload.Core.Common;
using Reload.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// The vertex array.
    /// </summary>
    public abstract class VertexArray : IBindable, IDisposable
    {
        /// <summary>
        /// Gets or sets the index buffer.
        /// </summary>
        public IndexBuffer IndexBuffer { get; protected set; }

        /// <summary>
        /// Gets or sets the vertex buffers.
        /// </summary>
        public List<VertexBuffer> VertexBuffers { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexArray"/> class.
        /// </summary>
        public VertexArray()
        {
            VertexBuffers = new List<VertexBuffer>();
        }

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

        /// <summary>
        /// Adds a vertex buffer.
        /// </summary>
        /// <param name="vertexBuffer">The vertex buffer.</param>
        public abstract void AddVertexBuffer(VertexBuffer vertexBuffer);

        /// <summary>
        /// Sets the index buffer.
        /// </summary>
        /// <param name="indexBuffer">The index buffer.</param>
        public abstract void SetIndexBuffer(IndexBuffer indexBuffer);

        /// <summary>
        /// Creates a new vertex array.
        /// </summary>
        /// <returns>A VertexArray.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VertexArray Create()
        {
            return GraphicsAPI.BufferFactory?.CreateVertexArray()
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

using System;
using System.Runtime.CompilerServices;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    public enum VertexBufferUsage
    {
        None = 0,
        Static,
        Dynamic
    }

    public delegate VertexBuffer CreateVertexBufferDelegate(
        Span<float> data, 
        BufferLayout layout, 
        VertexBufferUsage usage);

    public delegate VertexBuffer CreateEmptyVertexBufferDelegate(
        uint size, 
        BufferLayout layout, 
        VertexBufferUsage usage = VertexBufferUsage.Dynamic);

    /// <summary>
    /// The vertex buffer.
    /// </summary>
    public abstract class VertexBuffer : IBuffer, IDisposable
    {
        protected static CreateVertexBufferDelegate _createVertexBuffer;

        protected static CreateEmptyVertexBufferDelegate _createEmptyVertexBuffer;

        /// <summary>
        /// Gets the vertex shader data input layout.
        /// </summary>
        public BufferLayout Layout { get; }

        /// <summary>
        /// Factory method delegate for creating a new vertex buffer with predefined data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>VertexBuffer filled with the data passed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VertexBuffer Create(
            Span<float> data,
            BufferLayout layout,
            VertexBufferUsage usage = VertexBufferUsage.Static)
        {
            return _createVertexBuffer?.Invoke(data, layout, usage);
        }

        /// <summary>
        /// Factory method delegate for creating a new empty vertex buffer.
        /// </summary>
        /// <param name="size">The buffer size.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>Empty VertexBuffer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VertexBuffer Create(
            uint size,
            BufferLayout layout,
            VertexBufferUsage usage = VertexBufferUsage.Dynamic)
        {
            return _createEmptyVertexBuffer?.Invoke(size, layout, usage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBuffer"/> class
        /// with shader layout defined by the layout parameter.
        /// </summary>
        public VertexBuffer(BufferLayout layout)
        {
            Layout = layout;
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

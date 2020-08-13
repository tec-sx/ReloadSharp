using System;

namespace Reload.Rendering.Buffers
{
    public enum VertexBufferUsage
    {
        None = 0,
        Static,
        Dynamic
    }

    public delegate VertexBuffer CreateVertexBufferDelegate(
        Span<float> data, 
        BufferLayoutCollection layout, 
        VertexBufferUsage usage = VertexBufferUsage.Static);

    public delegate VertexBuffer CreateEmptyVertexBufferDelegate(
        uint size, 
        BufferLayoutCollection layout, 
        VertexBufferUsage usage = VertexBufferUsage.Dynamic);

    /// <summary>
    /// The vertex buffer.
    /// </summary>
    public abstract class VertexBuffer : IDisposable
    {
        public static CreateVertexBufferDelegate Create;

        public static CreateEmptyVertexBufferDelegate CreateEmpty;

        /// <summary>
        /// Gets the vertex shader data input layout.
        /// </summary>
        public BufferLayoutCollection Layout { get; }

        /// <summary>
        /// Binds the vertex buffer for usage.
        /// </summary>
        public abstract void Bind();

        /// <summary>
        /// Unbinds the vertex buffer.
        /// </summary>
        public abstract void Unbind();

        /// <summary>
        /// Disposes the class and cleans up resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBuffer"/> class.
        /// </summary>
        public VertexBuffer(BufferLayoutCollection layout)
        {
            Layout = layout;
        }
    }
}

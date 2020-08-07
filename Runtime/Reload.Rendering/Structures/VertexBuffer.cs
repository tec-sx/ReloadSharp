using System;

namespace Reload.Rendering.Structures
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
        VertexBufferUsage usage = VertexBufferUsage.Static);

    public delegate VertexBuffer CreateEmptyVertexBufferDelegate(
        uint size, 
        BufferLayout layout, 
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
        public BufferLayout Layout { get; }

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
        public VertexBuffer(BufferLayout layout)
        {
            Layout = layout;
        }
    }
}

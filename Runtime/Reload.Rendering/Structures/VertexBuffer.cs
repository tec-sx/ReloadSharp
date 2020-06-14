namespace Reload.Rendering.Structures
{
    using System;

    public delegate VertexBuffer CreateVertexBufferDelegate(Span<float> vertices);

    public abstract class VertexBuffer : IDisposable
    {
        public abstract void Bind();
        public abstract void Unbind();
        public abstract void Dispose();
        public abstract BufferLayout GetLayout();
        public abstract void SetLayout(BufferLayout layout);

        public static CreateVertexBufferDelegate Create;
    }
}

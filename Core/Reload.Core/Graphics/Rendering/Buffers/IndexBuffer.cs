namespace Reload.Core.Graphics.Rendering.Buffers
{
    using System;

    public delegate IndexBuffer CreateIndexBufferDelegate(Span<uint> indices);

    public abstract class IndexBuffer : IDisposable
    {
        public uint Count { get; protected set; }

        public abstract void Bind();
        public abstract void Unbind();
        public abstract void Dispose();

        public static CreateIndexBufferDelegate Create;
    }
}

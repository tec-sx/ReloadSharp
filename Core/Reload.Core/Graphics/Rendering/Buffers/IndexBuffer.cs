using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    public abstract class IndexBuffer : IDisposable
    {
        public uint Count { get; protected set; }

        public abstract void Bind();
        public abstract void Unbind();
        public abstract void Dispose();

        public static IndexBuffer Create(Span<uint> indices)
        {
            return BufferFactory.Create().IndexBuffer(indices);
        }
    }
}

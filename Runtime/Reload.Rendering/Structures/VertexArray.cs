using System.Collections.Generic;

namespace Reload.Rendering.Structures
{
    public delegate VertexArray CreateVertexArrayDelegate();

    public abstract class VertexArray
    {
        public IndexBuffer IndexBuffer { get; protected set; }
        public List<VertexBuffer> VertexBuffers { get; protected set; }

        public abstract void Bind();
        public abstract void Unbind();
        public abstract void Dispose();
        public abstract void AddVertexBuffer(VertexBuffer vertexBuffer);
        public abstract void SetIndexBuffer(IndexBuffer indexBuffer);

        public static CreateVertexArrayDelegate Create;
    }
}

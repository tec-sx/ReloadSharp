namespace Reload.Rendering
{
    using Reload.Rendering.Structures;
    using System.Drawing;

    public abstract class RendererApi
    {
        public abstract void SetClearColor(Color color);
        public abstract void Clear();
        public abstract void DrawIndexed(VertexArray vertexArray);
        public abstract void SetViewport(Size size);
    }
}

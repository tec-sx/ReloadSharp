namespace Reload.Rendering
{
    using System.Drawing;
    using Reload.Rendering.Structures;

    public delegate void ParameterlessDelegate();
    public delegate void VertexArrayDrawDelegate(VertexArray vertexArray);
    public delegate void SetColorDelegate(Color color);
    public delegate void SetSizeDelegate(Size size);

    public static class RenderCommand
    {
        public static SetSizeDelegate SetViewport;

        public static SetColorDelegate SetClearColor;

        public static ParameterlessDelegate Clear;

        public static VertexArrayDrawDelegate DrawIndexed;
    }
}

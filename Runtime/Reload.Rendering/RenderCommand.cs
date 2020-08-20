namespace Reload.Rendering
{
    using System.Drawing;
    using Reload.Core.Graphics.Rendering.Buffers;

    public delegate void ParameterlessDelegate();
    public delegate void VertexArrayDrawDelegate(VertexArray vertexArray);
    public delegate void SetColorDelegate(Color color);
    public delegate void SetSizeDelegate(Size size);
    public delegate void SetSizeAndLocationDelegate(Point location, Size size);

    public static class RenderCommand
    {
        public static ParameterlessDelegate Initialize;

        public static SetSizeDelegate SetViewportSize;

        public static SetSizeAndLocationDelegate SetViewportSizeAndLocation;
        
        public static SetColorDelegate SetClearColor;

        public static ParameterlessDelegate Clear;

        public static VertexArrayDrawDelegate DrawIndexed;
    }
}

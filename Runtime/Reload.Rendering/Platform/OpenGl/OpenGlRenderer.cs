namespace Reload.Rendering.Platform.OpenGl
{
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    public class OpenGlRenderer : RendererApi
    {
        public static GL Api { get; private set; }

        public OpenGlRenderer(IWindow window)
        {
            Api = GL.GetApi(window);
        }

        public override void Clear()
        {
            Api.Clear(
                (uint)ClearBufferMask.ColorBufferBit |
                (uint)ClearBufferMask.DepthBufferBit);
        }

        public override void SetClearColor(Color color)
        {
            Api.ClearColor(color);
        }

        public override void DrawIndexed(VertexArray vertexArray)
        {
            Api.DrawElements(
                PrimitiveType.Triangles,
                vertexArray.IndexBuffer.Count,
                DrawElementsType.UnsignedInt,
                null);
        }
    }
}

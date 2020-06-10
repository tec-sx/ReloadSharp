namespace Reload.Rendering
{
    using Silk.NET.Windowing.Common;
    using Reload.Rendering.Structures;
    using System.Drawing;

    public static class Renderer
    {
        public static void Initialize(IWindow window)
        {
            RenderCommand.Initialize(window);
        }

        public static void ShutDown()
        {

        }

        public static void OnWindowResize(Size size)
        {
            RenderCommand.SetViewport(size);
        }

        public static void BeginScene()
        {

        }

        public static void EndScene()
        {

        }

        public static void Submit(VertexArray vertexArray)
        {
            vertexArray.Bind();
            RenderCommand.DrawIndexed(vertexArray);
        }
    }
}

namespace Reload.Rendering
{
    using Reload.Rendering.Structures;
    using System.Drawing;

    public static class Renderer
    {
        public static void Initialize()
        {

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

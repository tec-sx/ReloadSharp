namespace Reload.Rendering
{
    using Reload.Rendering.Platform.OpenGl;
    using Silk.NET.Windowing.Common;
    using System;

    public static class Renderer
    {
        public static ContextAPI Api { get; private set; }

        public static void Initialize(IWindow window)
        {
            Api = window.API.API;

            RendererApi renderingApi;

            if (Api == ContextAPI.OpenGL || Api == ContextAPI.OpenGLES)
            {
                renderingApi = new OpenGlRenderer(window);
            }
            else if (Api == ContextAPI.Vulkan)
            {
                throw new NotImplementedException("Selected api is not implemented.");
            }
            else
            {
                throw new NotImplementedException("Selected api is not implemented.");
            }

            RenderCommand.Initialize(renderingApi);
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

namespace Reload.Rendering
{
    using Reload.Rendering.Platform.OpenGl;
    using Reload.Rendering.Platform.Vulkan;
    using Silk.NET.Windowing.Common;
    using Reload.Rendering.Structures;
    using System;
    using System.Drawing;

    public abstract class RendererApi
    {
        internal static ContextAPI Api { get; private set; }

        public abstract void SetClearColor(Color color);
        public abstract void Clear();
        public abstract void DrawIndexed(VertexArray vertexArray);
        public abstract void SetViewport(Size size);

        public static RendererApi Create(IWindow window)
        {
            Api = window.API.API;

            if (Api == ContextAPI.OpenGL || Api == ContextAPI.OpenGLES)
            {
                return new GlRenderer(window);
            }
            else if (Api == ContextAPI.Vulkan)
            {
                return new VulkanRenderer(window);
            }
            else
            {
                throw new ApplicationException(Properties.Resources.BackendNotSupportedError);
            }
        }
    }
}

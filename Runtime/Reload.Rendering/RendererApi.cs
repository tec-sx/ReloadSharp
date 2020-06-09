namespace Reload.Rendering
{
    using Reload.Rendering.Platform.OpenGl;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Drawing;

    public abstract class RendererApi
    {
        public static ContextAPI Api { get; private set; }

        public abstract void SetClearColor(Color color);
        public abstract void Clear();
        public abstract void DrawIndexed(VertexArray vertexArray);

        public static RendererApi Create(IWindow window)
        {
            return Api switch
            {
                ContextAPI.None => throw new ApplicationException(),
                ContextAPI.OpenGL => new OpenGlRenderer(window),
                ContextAPI.OpenGLES => new OpenGlRenderer(window),
                ContextAPI.Vulkan => throw new ApplicationException(),
                _ => throw new ApplicationException(),
            };
        }
    }
}

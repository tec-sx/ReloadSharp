namespace Reload.Rendering.Structures
{
    using Reload.Rendering.Platform.OpenGl;
    using Silk.NET.Windowing.Common;
    using System;

    public abstract class VertexBuffer : IDisposable
    {
        public abstract void Bind();
        public abstract void Unbind();
        public abstract void Dispose();

        public static VertexBuffer Create(Span<float> vertices)
        {
            return RendererApi.Api switch
            {
                ContextAPI.OpenGL => new GlVertexBuffer(vertices),
                ContextAPI.OpenGLES => new GlVertexBuffer(vertices),
                ContextAPI.Vulkan => throw new ApplicationException(Properties.Resources.BackendNotSupportedError),
                ContextAPI.None => throw new ApplicationException(Properties.Resources.BackendNotSupportedError)
            };
        }
    }
}

namespace Reload.Rendering.Buffers
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
            return Renderer.Api switch
            {
                ContextAPI.OpenGL => new OpenGlVertexBuffer(vertices),
                ContextAPI.OpenGLES => new OpenGlVertexBuffer(vertices),
                _ => throw new ApplicationException("Graphics backend not supported")
            };
        }
    }
}

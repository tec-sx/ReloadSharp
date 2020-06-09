namespace Reload.Rendering.Buffers
{
    using Reload.Rendering.Platform.OpenGl;
    using Silk.NET.Windowing.Common;
    using System;

    public abstract class IndexBuffer : IDisposable
    {
        public abstract void Bind();
        public abstract void Unbind();
        public abstract void Dispose();

        public static IndexBuffer Create(Span<uint> indices)
        {
            return Renderer.Api switch
            {
                ContextAPI.OpenGL => new OpenGlIndexBuffer(indices),
                ContextAPI.OpenGLES => new OpenGlIndexBuffer(indices),
                _ => throw new ApplicationException("Graphics backend not supported")
            };
        }
    }
}

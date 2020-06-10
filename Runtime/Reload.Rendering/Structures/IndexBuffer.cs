namespace Reload.Rendering.Structures
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
            return RendererApi.Api switch
            {
                ContextAPI.OpenGL => new GlIndexBuffer(indices),
                ContextAPI.OpenGLES => new GlIndexBuffer(indices),
                ContextAPI.Vulkan => throw new ApplicationException(Properties.Resources.BackendNotSupportedError),
                ContextAPI.None => throw new ApplicationException(Properties.Resources.BackendNotSupportedError)
            };
        }
    }
}

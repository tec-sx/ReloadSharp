using System;
using Silk.NET.Core.Contexts;
using Silk.NET.Windowing.Common;
using Reload.Platform.Graphics.OpenGl.Buffers;
using Silk.NET.OpenGL;
using Reload.Core.Graphics;
using Reload.Platform.Graphics.OpenGl.Shaders;
using Reload.Platform.Graphics.OpenGl.Textures;
using Reload.Platform.Graphics.OpenGl.Renderer;

namespace Reload.Platform.Graphics.OpenGl
{
    /// <summary>
    /// The PenGl API.
    /// </summary>
    public class OpenGlAPI : Core.Graphics.GraphicsAPI
    {
        private GL _api { get; init; }

        private bool _disposed;

        private OpenGlAPI()
        { }
        
        public OpenGlAPI(IWindow window)
            : this(window.GLContext)
        { }
        
        public OpenGlAPI(INativeContext context)
            : base(GraphicsAPIType.OpenGL, new GraphicsAPIVersion())
        {
            _api = GL.GetApi(context);
        }

        public OpenGlAPI(Func<string, IntPtr> getProcAddress)
            : base(GraphicsAPIType.OpenGL, new GraphicsAPIVersion())
        {
            _api = GL.GetApi(getProcAddress);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            BufferFactory = new OpenGlBufferFactory(_api);
            ShaderFactory = new OpenGlShaderFactory(_api);
            TextureFactory = new OpenGlTextureFactory(_api);
            RendererFactory = new OpenGlRendererFactory(_api);
        }

        /// <inheritdoc/>
        public override void ShutDown()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}

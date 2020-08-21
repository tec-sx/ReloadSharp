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
        private readonly GL _api;

        private bool _disposed;

        /// <summary>
        /// Prevents a default instance of the <see cref="OpenGlAPI"/> class from being created.
        /// </summary>
        private OpenGlAPI()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlAPI"/> class.
        /// </summary>
        /// <param name="window">The window to bind to.</param>
        public OpenGlAPI(ProgramWindow window)
            : base(GraphicsAPIType.OpenGL, new GraphicsAPIVersion())
        {
            _api = GL.GetApi(window.GetProcAddress);
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
        { }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _api.Dispose();
            }

            _disposed = true;
        }
    }
}

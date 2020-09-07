using Reload.Platform.Graphics.OpenGl.Buffers;
using Silk.NET.OpenGL;
using Reload.Core.Graphics;
using Reload.Platform.Graphics.OpenGl.Shaders;
using Reload.Platform.Graphics.OpenGl.Textures;
using Reload.Platform.Graphics.OpenGl.Renderer;

using GraphicsAPI = Reload.Core.Graphics.GraphicsAPI;
using Reload.Core.Windowing;

namespace Reload.Platform.Graphics.OpenGl
{
    /// <summary>
    /// The PenGl API.
    /// </summary>
    public class OpenGlAPI : GraphicsAPI
    {
        private GL _api;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlAPI"/> class.
        /// </summary>
        public OpenGlAPI()
            : base(GraphicsAPIType.OpenGL, new GraphicsAPIVersion())
        { }

        /// <inheritdoc/>
        public override void Configure(IProgramWindow window)
        {
            _api = GL.GetApi(window.GetProcAddress);

            BufferFactory = new OpenGlBufferFactory(_api);
            ShaderFactory = new OpenGlShaderFactory(_api);
            TextureFactory = new OpenGlTextureFactory(_api);
            RendererFactory = new OpenGlRendererFactory(_api);
        }

        /// <inheritdoc/>
        public override void StartUp()
        {

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

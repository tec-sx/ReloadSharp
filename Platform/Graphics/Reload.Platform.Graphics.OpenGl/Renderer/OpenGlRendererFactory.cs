using Reload.Core.Graphics.Rendering;
using Silk.NET.OpenGL;

namespace Reload.Platform.Graphics.OpenGl.Renderer
{
    /// <summary>
    /// The OpenGl render service.
    /// </summary>
    internal class OpenGlRendererFactory : RenderFactory
    {
        private GL _api;

        public OpenGlRendererFactory(GL api)
        {
            _api = api;
        }
    }
}

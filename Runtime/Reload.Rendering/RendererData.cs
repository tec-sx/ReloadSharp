using Reload.Rendering.Buffers;
using Reload.Rendering.Shaders;

namespace Reload.Rendering
{
    public struct RendererData
    {
        public RenderPass ActiveRenderPass { get; set; }

        internal RenderCommandQueue CommandQueue { get; set; }
        
        public ShaderLibrary ShaderLibrary { get; set; }
        
        public VertexArray FullScreenQuadVertexArray { get; set; }
    }
}

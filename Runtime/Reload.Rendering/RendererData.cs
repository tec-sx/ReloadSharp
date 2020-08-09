using Reload.Rendering.Structures;

namespace Reload.Rendering
{
    public struct RendererData
    {
        public RenderPass ActiveRenderPass { get; set; }

        public RenderCommandQueue CommandQueue { get; set; }
        
        public ShaderLibrary ShaderLibrary { get; set; }
        
        public VertexArray FullScreenQuadVertexArray { get; set; }
    }
}

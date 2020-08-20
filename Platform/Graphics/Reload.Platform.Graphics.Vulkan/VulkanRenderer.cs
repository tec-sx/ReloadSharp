namespace Reload.Rendering.Platform.Vulkan
{
    using Reload.Rendering.Buffers;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    /// <summary>
    /// The vulkan renderer.
    /// </summary>
    public class VulkanRenderer : RendererAPI
    {
        public VulkanRenderer(IWindow window)
        {

        }

        public override void Clear(Color color)
        {
            throw new System.NotImplementedException();
        }

       
        public override void DrawIndexed(uint count, PrimitiveType type, bool depthTest = true)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void SetLineThickness(float thickness)
        {
            throw new System.NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new System.NotImplementedException();
        }
    }
}

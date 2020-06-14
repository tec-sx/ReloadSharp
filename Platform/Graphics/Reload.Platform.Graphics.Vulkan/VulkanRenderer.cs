namespace Reload.Rendering.Platform.Vulkan
{
    using Reload.Rendering.Structures;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    public class VulkanRenderer : RenderingApi
    {
        public VulkanRenderer(IWindow window)
        {

        }
        public override void Clear()
        {
            throw new System.NotImplementedException();
        }

        public override void DrawIndexed(VertexArray vertexArray)
        {
            throw new System.NotImplementedException();
        }

        public override void SetClearColor(Color color)
        {
            throw new System.NotImplementedException();
        }

        public override void SetViewport(Size size)
        {
            throw new System.NotImplementedException();
        }
    }
}

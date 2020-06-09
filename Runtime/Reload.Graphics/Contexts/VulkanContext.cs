namespace Reload.Graphics.Contexts
{
    using Silk.NET.Vulkan;
    using Silk.NET.Windowing.Common;

    public class VulkanContext : IGraphicsContext
    {
        private Vk _api;

        public VulkanContext(IWindow window)
        {
            _api = Vk.GetApi();
        }
    }
}

namespace Core.CoreSystem.Graphics
{
    using Silk.NET.Vulkan;

    public struct SwapChainSupportDetails
    {
        public SurfaceCapabilitiesKHR Capabilities { get; set; }
        public SurfaceFormatKHR[] Formats { get; set; }
        public PresentModeKHR[] PresentModes { get; set; }
    }

    public struct QueueFamilyIndices
    {
        public uint? GraphicsFamily { get; set; }
        public uint? PresentFamily { get; set; }

        public bool IsComplete()
        {
            return GraphicsFamily.HasValue && PresentFamily.HasValue;
        }
    }
}
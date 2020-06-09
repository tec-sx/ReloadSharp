namespace Reload.Graphics.Contexts
{
    using Silk.NET.Windowing.Common;
    using System;

    public static class GraphicsContextFactory
    {
        public static IGraphicsContext CreateContext(IWindow window)
        {
            if (window.API.API == ContextAPI.OpenGL || window.API.API == ContextAPI.OpenGL)
            {
                return new OpenGl(window);
            }
            else if (window.API.API == ContextAPI.Vulkan)
            {
                return new VulkanContext(window);
            }
            else
            {
                throw new NotImplementedException("Selected api is not implemented.");
            }
        }
    }
}

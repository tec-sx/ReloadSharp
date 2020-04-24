namespace Core.Tests.CoreSystem
{
    using Core.Config;
    using Core.CoreSystem.Graphics;
    using Silk.NET.Windowing.Common;
    using Xunit;

    public class GraphicsManagerTests
    {
        [Fact]
        public void CreateWindowVulkanSuccess()
        {
            #region Arrange
            Configuration.LoadDefaultConfiguration();
            Configuration.Settings.Display.UseVulkan = true;
            #endregion

            #region Act
            var graphicsManager = new GraphicsManager();
            graphicsManager.CreateWindow();
            #endregion

            #region Assert
            Assert.Equal(ContextAPI.Vulkan, graphicsManager.Window.API.API);
            #endregion

        }

        [Fact]
        public void CreateWindowOpenGlSuccess()
        {
            #region Arrange
            Configuration.LoadDefaultConfiguration();
            Configuration.Settings.Display.UseVulkan = false;
            #endregion

            #region Act
            var graphicsManager = new GraphicsManager();
            graphicsManager.CreateWindow();
            #endregion

            #region Assert
            Assert.InRange(graphicsManager.Window.API.API, ContextAPI.OpenGL, ContextAPI.OpenGLES);
            #endregion

        }
    }
}

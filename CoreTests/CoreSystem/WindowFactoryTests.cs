using Core.Config;
using Core.CoreSystem.Graphics;
using Silk.NET.Windowing.Common;
using System.Collections.Generic;
using Xunit;

namespace CoreTests.CoreSystem
{
    public class WindowFactoryTests
    {
        [Fact]
        public void CreateWindowVulkanSuccess()
        {
            #region Arrange

            Configuration.LoadDefaultConfiguration();
            Configuration.Settings.Display.UseVulkan = true;

            #endregion

            #region Act

            var window = WindowFactory.CreateWindow();

            #endregion

            #region Assert

                Assert.Equal(ContextAPI.Vulkan, window.API.API);

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

            var window = WindowFactory.CreateWindow();

            #endregion

            #region Assert

            Assert.InRange(window.API.API, ContextAPI.OpenGL, ContextAPI.OpenGLES);

            #endregion

        }
    }
}

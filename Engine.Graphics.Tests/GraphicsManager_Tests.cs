using FluentAssertions;
using NUnit.Framework;
using Silk.NET.Windowing.Common;
using System.Drawing;

namespace Engine.Graphics.Tests
{
    public class GraphicsManager_Tests
    {
        private DisplayConfiguration _displayConfiguration;
        [SetUp]
        public void Setup()
        {
            _displayConfiguration = new DisplayConfiguration
            {
                Resolution = new Point(1280, 760),
                RefreshRate = 60,
                TargetFps = 60,
                InFullScreen = false,
                EnableVSync = true,
                EnableVulkan = false,
                WindowTitle = "Reload graphics engine tests."
            };
        }

        [Test]
        public void Create_Vulkan_Window_Success()
        {
            #region Arrange
            _displayConfiguration.EnableVulkan = true;
            #endregion

            #region Act
            var graphicsManager = new GraphicsManager(_displayConfiguration);
            graphicsManager.CreateWindow();
            #endregion

            #region Assert
            graphicsManager.Window.API.API.Should().Be(ContextAPI.Vulkan);
            #endregion

        }

        [Test]
        public void Create_OpenGl_Window_Success()
        {
            #region Arrange
            _displayConfiguration.EnableVulkan = false;
            #endregion

            #region Act
            var graphicsManager = new GraphicsManager(_displayConfiguration);
            graphicsManager.CreateWindow();
            #endregion

            #region Assert
            graphicsManager.Window.API.API
                .Should().Match<ContextAPI>(api => api == ContextAPI.OpenGL || api == ContextAPI.OpenGLES);
            #endregion

        }
    }
}
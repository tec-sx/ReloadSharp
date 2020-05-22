using FluentAssertions;
using NUnit.Framework;
using Silk.NET.Windowing.Common;
using System.Drawing;

namespace Engine.Graphics.Tests
{
    public class GraphicsManager_Tests
    {
        private DisplayConfiguration displayConfiguration;

        [SetUp]
        public void Setup()
        {
            displayConfiguration = new DisplayConfiguration
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
        public void Create_OpenGl_Window_Success()
        {
            // Arrange
            displayConfiguration.EnableVulkan = false;

            // Act
            var graphicsManager = new GraphicsManager();
            var window = graphicsManager.CreateWindow(displayConfiguration);

            // Assert
            window.API.API
                .Should().Match<ContextAPI>(api => api == ContextAPI.OpenGL || api == ContextAPI.OpenGLES);

        }

        [Test]
        public void Compile_Shader_Program_Success()
        {

        }
    }
}
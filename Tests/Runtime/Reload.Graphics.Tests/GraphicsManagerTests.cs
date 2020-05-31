namespace Reload.Graphics.Tests
{
    using FluentAssertions;
    using Xunit;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    public class GraphicsManagerTests
    {
        private DisplayConfiguration displayConfiguration = new DisplayConfiguration
        {
            Resolution = new Point(1280, 760),
            RefreshRate = 60,
            TargetFps = 60,
            InFullScreen = false,
            EnableVSync = true,
            EnableVulkan = false,
            WindowTitle = "Reload graphics engine tests."
        };

        [Fact]
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

        [Fact]
        public void Compile_Shader_Program_Success()
        {

        }
    }
}
using FluentAssertions;
using Reload.Core.Exceptions;
using Reload.Core.Graphics.Rendering.Shaders;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests.Graphics.Rendering
{
    public class ShaderTests
    {
        [Fact]
        public void ShaderProgram_Constraints()
        {
            Type shaderProgram = typeof(ShaderProgram);

            shaderProgram
                .Should().BeAbstract()
                .And.Implement<IDisposable>()
                .And.HaveDefaultConstructor();
        }

        [Fact]
        public void CreateShader_FactoryNotImplemented_ThrowsFactoryNotImplementedException()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutShaderFactoryImplementation();

            // Act
            Func<ShaderProgram> createShaderProgramAct = () => ShaderProgram.Create("filename", null);

            //Assert
            createShaderProgramAct.Should().Throw<ReloadFactoryNotImplementedException>();
        }

        [Fact]
        public void CreateShader_FactoryImplemented_ReturnsIndexBuffer()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithShaderFactoryImplementation();

            // Act
            Func<ShaderProgram> createShaderProgramAct = () => ShaderProgram.Create("filename", null);

            ShaderProgram shaderProgram = createShaderProgramAct?.Invoke();

            //Assert
            createShaderProgramAct.Should().NotThrow();
            shaderProgram.Should().NotBeNull();
        }
    }
}

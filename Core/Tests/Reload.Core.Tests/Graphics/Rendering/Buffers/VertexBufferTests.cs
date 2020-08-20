using FluentAssertions;
using Reload.Core.Exceptions;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests.Graphics.Rendering.Buffers
{
    public class VertexBufferTests
    {
        [Fact]
        public void Create_FactoryNotImplemented_ThrowsFactoryNotImplementedException()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutBufferFactoryImplementation();
            BufferLayout layout = new BufferLayout();

            // Act
            Func<VertexBuffer> createEmptyVertexBufferAct = () => VertexBuffer.Create(0, layout);
            Func<VertexBuffer> createWithDataVertexBufferAct = () => VertexBuffer.Create(new Span<float>(), layout);

            //Assert
            createEmptyVertexBufferAct.Should().Throw<ReloadFactoryNotImplementedException>();
            createWithDataVertexBufferAct.Should().Throw<ReloadFactoryNotImplementedException>();
        }

        [Fact]
        public void Create_FactoryImplemented_ReturnsVertexBuffer()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithBufferFactoryImplementation();
            BufferLayout layout = new BufferLayout();

            // Act
            Func<VertexBuffer> createEmptyVertexBufferAct = () => VertexBuffer.Create(0, layout);
            Func<VertexBuffer> createWithDataVertexBufferAct = () => VertexBuffer.Create(new Span<float>(), layout);

            VertexBuffer emptyVertexBuffer = createEmptyVertexBufferAct?.Invoke();
            VertexBuffer vertexBufferWithData = createWithDataVertexBufferAct?.Invoke();

            //Assert
            createEmptyVertexBufferAct.Should().NotThrow();
            createWithDataVertexBufferAct.Should().NotThrow();
            emptyVertexBuffer.Should().NotBeNull();
            vertexBufferWithData.Should().NotBeNull();
        }
    }
}

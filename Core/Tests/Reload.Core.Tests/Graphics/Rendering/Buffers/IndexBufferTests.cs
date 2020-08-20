using FluentAssertions;
using Reload.Core.Exceptions;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests.Graphics.Rendering.Buffers
{
    public class IndexBufferTests
    {
        [Fact]
        public void CreateIndexBuffer_FactoryNotImplemented_ThrowsFactoryNotImplementedException()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutBufferFactoryImplementation();

            // Act
            Func<IndexBuffer> createIndexBufferWithDataAct = () => IndexBuffer.Create(new Span<uint>());

            //Assert
            createIndexBufferWithDataAct.Should().Throw<ReloadFactoryNotImplementedException>();
        }

        [Fact]
        public void CreateIndexBuffer_FactoryImplemented_ReturnsIndexBuffer()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithBufferFactoryImplementation();

            // Act
            Func<IndexBuffer> createIndexBufferWithDataAct = () => IndexBuffer.Create(new Span<uint>());

            IndexBuffer indexBufferWithData = createIndexBufferWithDataAct?.Invoke();

            //Assert
            createIndexBufferWithDataAct.Should().NotThrow();
            indexBufferWithData.Should().NotBeNull();
        }
    }
}

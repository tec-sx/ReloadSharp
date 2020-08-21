using FluentAssertions;
using Reload.Core.Common;
using Reload.Core.Exceptions;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests.Graphics.Rendering
{
    public class BufferTests
    {
        [Fact]
        public void VertexBuffer_Contraints()
        {
            Type vertexBuffer = typeof(VertexBuffer);

            vertexBuffer
                .Should().BeAbstract()
                .And.Implement<IBindable>()
                .And.Implement<IDisposable>()
                .And.HaveDefaultConstructor();
        }

        [Fact]
        public void IndexBuffer_Contraints()
        {
            Type indexBuffer = typeof(IndexBuffer);

            indexBuffer
                .Should().BeAbstract()
                .And.Implement<IBindable>()
                .And.Implement<IDisposable>()
                .And.HaveDefaultConstructor();
        }

        [Fact]
        public void VertexArray_Contraints()
        {
            Type vertexArray = typeof(VertexArray);

            vertexArray
                .Should().BeAbstract()
                .And.Implement<IBindable>()
                .And.Implement<IDisposable>()
                .And.HaveDefaultConstructor();
        }

        [Fact]
        public void CreateVertexBuffer_FactoryNotImplemented_ThrowsFactoryNotImplementedException()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutBufferFactoryImplementation();
            BufferLayout layout = new BufferLayout();

            // Act
            Func<VertexBuffer> createEmptyVertexBufferAct = () => VertexBuffer.CreateEmpty(0, layout);
            Func<VertexBuffer> createWithDataVertexBufferAct = () => VertexBuffer.Create(new Span<float>(), layout);

            //Assert
            createEmptyVertexBufferAct.Should().Throw<ReloadFactoryNotImplementedException>();
            createWithDataVertexBufferAct.Should().Throw<ReloadFactoryNotImplementedException>();
        }

        [Fact]
        public void CreateVertexBuffer_FactoryImplemented_ReturnsVertexBuffer()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithBufferFactoryImplementation();
            BufferLayout layout = new BufferLayout();

            // Act
            Func<VertexBuffer> createEmptyVertexBufferAct = () => VertexBuffer.CreateEmpty(0, layout);
            Func<VertexBuffer> createWithDataVertexBufferAct = () => VertexBuffer.Create(new Span<float>(), layout);

            VertexBuffer emptyVertexBuffer = createEmptyVertexBufferAct?.Invoke();
            VertexBuffer vertexBufferWithData = createWithDataVertexBufferAct?.Invoke();

            //Assert
            createEmptyVertexBufferAct.Should().NotThrow();
            createWithDataVertexBufferAct.Should().NotThrow();

            emptyVertexBuffer.Should().NotBeNull();
            vertexBufferWithData.Should().NotBeNull();
        }

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

        [Fact]
        public void CreateVertexArray_FactoryNotImplemented_ThrowsFactoryNotImplementedException()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutBufferFactoryImplementation();

            // Act
            Func<VertexArray> createVertexArrayAct = () => VertexArray.Create();

            //Assert
            createVertexArrayAct.Should().Throw<ReloadFactoryNotImplementedException>();
        }

        [Fact]
        public void CreateVertexArray_FactoryImplemented_ReturnsIndexBuffer()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithBufferFactoryImplementation();

            // Act
            Func<VertexArray> createVertexArrayAct = () => VertexArray.Create();

            VertexArray vertexArray = createVertexArrayAct?.Invoke();

            //Assert
            createVertexArrayAct.Should().NotThrow();
            vertexArray.Should().NotBeNull();
        }
    }
}

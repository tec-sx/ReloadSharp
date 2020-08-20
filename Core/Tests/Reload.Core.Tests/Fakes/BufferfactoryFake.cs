using NSubstitute;
using Reload.Core.Graphics.Rendering.Buffers;
using System;

namespace Reload.Core.Tests.Fakes
{
    public class BufferfactoryFake : BufferFactory
    {
        protected override IndexBuffer CreateIndexBuffer(Span<uint> indices) => Substitute.For<IndexBuffer>();

        protected override VertexArray CreateVertexArray() => Substitute.For<VertexArray>();

        protected override VertexBuffer CreateVertexBuffer(Span<float> data, BufferLayout layout, VertexBufferUsage usage) => Substitute.For<VertexBuffer>(layout);

        protected override VertexBuffer CreateVertexBuffer(uint size, BufferLayout layout, VertexBufferUsage usage) => Substitute.For<VertexBuffer>(layout);
    }
}

using Reload.Core.Graphics.Rendering.Buffers;
using Silk.NET.OpenGL;
using System;

namespace Reload.Platform.Graphics.OpenGl.Buffers
{
    /// <summary>
    /// The open gl buffer factory.
    /// </summary>
    internal sealed class OpenGlBufferFactory : BufferFactory
    {
        private readonly GL _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlBufferFactory"/> class.
        /// </summary>
        /// <param name="api">The api.</param>
        public OpenGlBufferFactory(GL api) 
        { 
            _api = api; 
        }

        /// <inheritdoc/>
        protected override IndexBuffer CreateIndexBuffer(Span<uint> indices)
        {
            return new OpenGlIndexBuffer(indices, _api);
        }

        /// <inheritdoc/>
        protected override VertexArray CreateVertexArray()
        {
            return new OpenGlVertexArray(_api);
        }

        /// <inheritdoc/>
        protected override VertexBuffer CreateVertexBuffer(Span<float> data, BufferLayout layout, VertexBufferUsage usage)
        {
            return new OpenGlVertexBuffer(data, layout, _api);
        }

        /// <inheritdoc/>
        protected override VertexBuffer CreateVertexBuffer(uint size, BufferLayout layout, VertexBufferUsage usage)
        {
            throw new NotImplementedException();
        }
    }
}

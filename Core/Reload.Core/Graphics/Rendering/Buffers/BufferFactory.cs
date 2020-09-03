#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
using System;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// An graphic buffer factory used to abstract the creation of buffers
    /// from their implementation.
    /// </summary>
    public abstract class BufferFactory
    {
        /// <summary>
        /// Creates a new vertex buffer with predefined data.
        /// </summary>
        /// <param name="data">The buffer data.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>A VertexBuffer.</returns>
        protected internal abstract VertexBuffer CreateVertexBuffer(Span<float> data, BufferLayout layout, VertexBufferUsage usage);

        /// <summary>
        /// Creates a new empty vertex buffer.
        /// </summary>
        /// <param name="size">The buffer size.</param>
        /// <param name="layout">The buffer  layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>A VertexBuffer.</returns>
        protected internal abstract VertexBuffer CreateVertexBuffer(uint size, BufferLayout layout, VertexBufferUsage usage);

        /// <summary>
        /// Creates a new index buffer with predefined data.
        /// </summary>
        /// <param name="indices">The indices.</param>
        /// <returns>An IndexBuffer.</returns>
        protected internal abstract IndexBuffer CreateIndexBuffer(Span<uint> indices);

        /// <summary>
        /// Creates a new vertex array.
        /// </summary>
        /// <returns>A VertexArray.</returns>
        protected internal abstract VertexArray CreateVertexArray();

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Graphics buffers service.";
        }
    }
}

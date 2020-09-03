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
using Reload.Core.Common;
using Reload.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// The vertex array.
    /// </summary>
    public abstract class VertexArray : IBindable, IDisposable
    {
        /// <summary>
        /// Gets or sets the index buffer.
        /// </summary>
        public IndexBuffer IndexBuffer { get; protected set; }

        /// <summary>
        /// Gets or sets the vertex buffers.
        /// </summary>
        public List<VertexBuffer> VertexBuffers { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexArray"/> class.
        /// </summary>
        public VertexArray()
        {
            VertexBuffers = new List<VertexBuffer>();
        }

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

        /// <summary>
        /// Adds a vertex buffer.
        /// </summary>
        /// <param name="vertexBuffer">The vertex buffer.</param>
        public abstract void AddVertexBuffer(VertexBuffer vertexBuffer);

        /// <summary>
        /// Sets the index buffer.
        /// </summary>
        /// <param name="indexBuffer">The index buffer.</param>
        public abstract void SetIndexBuffer(IndexBuffer indexBuffer);

        /// <summary>
        /// Creates a new vertex array.
        /// </summary>
        /// <returns>A VertexArray.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VertexArray Create()
        {
            Debug.Assert(GraphicsAPI.BufferFactory != null);

            return GraphicsAPI.BufferFactory.CreateVertexArray();
        }

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

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
using System.Diagnostics;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// The vertex buffer.
    /// </summary>
    public abstract class VertexBuffer : IBindable, IDisposable
    {
        /// <summary>
        /// Gets the vertex shader data input layout.
        /// </summary>
        public BufferLayout Layout { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBuffer"/> class.
        /// </summary>
        protected VertexBuffer()
        {
            Layout = new BufferLayout();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBuffer"/> class
        /// with shader layout defined by the layout parameter.
        /// </summary>
        protected VertexBuffer(BufferLayout layout)
        {
            Layout = layout;
        }


        /// <summary>
        /// Factory method for creating a new vertex buffer with predefined data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>VertexBuffer filled with the data passed.</returns>
        public static VertexBuffer Create(
            Span<float> data,
            BufferLayout layout,
            VertexBufferUsage usage = VertexBufferUsage.Static)
        {
            Debug.Assert(GraphicsAPI.BufferFactory != null);

            return GraphicsAPI.BufferFactory.CreateVertexBuffer(data, layout, usage);
        }

        /// <summary>
        /// Factory method for creating a new empty vertex buffer.
        /// </summary>
        /// <param name="size">The buffer size.</param>
        /// <param name="layout">The buffer layout.</param>
        /// <param name="usage">The buffer usage.</param>
        /// <returns>Empty VertexBuffer.</returns>
        public static VertexBuffer CreateEmpty(
            uint size,
            BufferLayout layout,
            VertexBufferUsage usage = VertexBufferUsage.Dynamic)
        {
            Debug.Assert(GraphicsAPI.BufferFactory != null);

            return GraphicsAPI.BufferFactory.CreateVertexBuffer(size, layout, usage);
        }

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

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

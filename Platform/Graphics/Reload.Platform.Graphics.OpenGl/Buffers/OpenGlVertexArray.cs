using Reload.Core.Graphics.Rendering.Buffers;
using Silk.NET.OpenGL;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reload.Platform.Graphics.OpenGl.Buffers
{
    /// <summary>
    /// The OpenGL vertex array implementation.
    /// </summary>
    internal sealed class OpenGlVertexArray : VertexArray
    {
        private GL _gl;

        private uint _handle;

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlVertexArray"/> class.
        /// </summary>
        /// <param name="api">The api.</param>
        public OpenGlVertexArray(GL api)
        {
            _gl = api;
            _handle = _gl.CreateVertexArray();
        }

        /// <inheritdoc/>
        public override void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            _gl.BindVertexArray(_handle);
            indexBuffer.Bind();

            IndexBuffer = indexBuffer;
        }

        /// <inheritdoc/>
        public unsafe override void AddVertexBuffer(VertexBuffer vertexBuffer)
        {
            _gl.BindVertexArray(_handle);
            vertexBuffer.Bind();

            Debug.Assert(vertexBuffer.Layout.Count > 0, Properties.Resources.VertexBufferHasNoLayout);

            uint index = 0;
            var layout = vertexBuffer.Layout;

            foreach (var element in layout)
            {
                _gl.EnableVertexAttribArray(index);
                _gl.VertexAttribPointer(
                    index,
                    element.GetComponentCount(),
                    OpenGlUtilities.ShaderDataTypeToGlBaseType(element.Type),
                    element.Normalized,
                    layout.Stride,
                    (void *)element.Offset);
                index++;
            }

            VertexBuffers.Add(vertexBuffer);
        }

        /// <inheritdoc/>
        public override void Bind()
        {
            _gl.BindVertexArray(_handle);
        }

        /// <inheritdoc/>
        public override void Unbind()
        {
            _gl.BindVertexArray(0);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                _gl.DeleteBuffer(_handle);
            }

            _isDisposed = true;
        }
    }
}

using Reload.Core.Graphics.Rendering.Buffers;
using Silk.NET.OpenGL;
using System;

namespace Reload.Platform.Graphics.OpenGl.Structures
{
    /// <summary>
    /// The OpenGL vertex buffer.
    /// </summary>
    public sealed class OpenGlVertexBuffer : VertexBuffer
    {
        /// <summary>
        /// The OpenGl buffer type constant set to array buffer.
        /// </summary>
        private const BufferTargetARB BufferType = BufferTargetARB.ArrayBuffer;

        private readonly GL _gl;

        private readonly uint _handle;

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlVertexBuffer"/> class.
        /// </summary>
        /// <param name="data">The buffer data.</param>
        /// <param name="api">The OpenGl api handle.</param>
        public unsafe OpenGlVertexBuffer(Span<float> data, BufferLayout layout, GL api)
            : base(layout)
        {
            _gl = api;
            _handle = _gl.CreateBuffer();
            _gl.BindBuffer(BufferType, _handle);

            fixed (void* dataPtr = data)
            {
                _gl.BufferData(
                    BufferType,
                    (UIntPtr)(data.Length * sizeof(float)),
                    dataPtr,
                    BufferUsageARB.StaticDraw);
            }
        }

        /// <inheritdoc/>
        public override void Bind()
        {
            _gl.BindBuffer(BufferType, _handle);
        }

        /// <inheritdoc/>
        public override void Unbind()
        {
            _gl.BindBuffer(BufferType, 0);
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

using Reload.Core.Graphics.Rendering.Buffers;
using Silk.NET.OpenGL;
using System;

namespace Reload.Platform.Graphics.OpenGl.Buffers
{
    /// <summary>
    /// The OpenGl index buffer.
    /// </summary>
    public class OpenGlIndexBuffer : IndexBuffer
    {
        /// <summary>
        /// The buffer type.
        /// </summary>
        private const BufferTargetARB BufferType = BufferTargetARB.ElementArrayBuffer;

        private readonly GL _gl;

        private uint _handle;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlIndexBuffer"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="api">The api.</param>
        public unsafe OpenGlIndexBuffer(Span<uint> data, GL api)
        {
            _gl = api;
            _handle = _gl.CreateBuffer();
            _gl.BindBuffer(BufferType, _handle);

            Count = (uint)data.Length;

            fixed (void* dataPtr = data)
            {
                _gl.BufferData(
                    BufferType,
                    (UIntPtr)(Count * sizeof(uint)),
                    dataPtr,
                    BufferUsageARB.StaticDraw);
            }
        }

        /// <inheritdoc/>
        public override void Bind()
        {
            _gl.BindBuffer(BufferType, _handle);
        }

        public override void Unbind()
        {
            _gl.BindBuffer(BufferType, 0);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            { }

            _gl.DeleteBuffer(_handle);

            _disposed = true;
        }
    }
}

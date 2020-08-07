using Reload.Rendering.Structures;
using Silk.NET.OpenGL;
using System;

namespace Reload.Platform.Graphics.OpenGl.Structures
{
    /// <summary>
    /// The OpenGL vertex buffer.
    /// </summary>
    public sealed class GlVertexBuffer : VertexBuffer
    {
        /// <summary>
        /// The OpenGl buffer type constant set to array buffer.
        /// </summary>
        private const BufferTargetARB BufferType = BufferTargetARB.ArrayBuffer;

        private readonly GL _gl;

        private readonly uint _handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlVertexBuffer"/> class.
        /// </summary>
        /// <param name="data">The buffer data.</param>
        /// <param name="api">The OpenGl api handle.</param>
        public unsafe GlVertexBuffer(Span<float> data, BufferLayout layout, GL api)
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
        public override void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}

namespace Reload.Platform.Graphics.OpenGl.Structures
{
    using Reload.Rendering.Buffers;
    using Silk.NET.OpenGL;
    using System;

    public class GlIndexBuffer : IndexBuffer
    {
        private const BufferTargetARB _bufferType = BufferTargetARB.ElementArrayBuffer;

        private GL _gl;
        private uint _handle;

        public unsafe GlIndexBuffer(Span<uint> data, GL api)
        {
            _gl = api;
            _handle = _gl.CreateBuffer();
            _gl.BindBuffer(_bufferType, _handle);

            Count = (uint)data.Length;

            fixed (void* dataPtr = data)
            {
                _gl.BufferData(
                    _bufferType,
                    (UIntPtr)(Count * sizeof(uint)),
                    dataPtr,
                    BufferUsageARB.StaticDraw);
            }
        }

        public override void Bind()
        {
            _gl.BindBuffer(_bufferType, _handle);
        }

        public override void Unbind()
        {
            _gl.BindBuffer(_bufferType, 0);
        }

        public override void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}

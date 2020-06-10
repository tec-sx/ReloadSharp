﻿namespace Reload.Rendering.Platform.OpenGl
{
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using System;

    public class GlVertexBuffer : VertexBuffer
    {
        private const BufferTargetARB _bufferType = BufferTargetARB.ArrayBuffer;

        private GL _gl;
        private uint _handle;

        public unsafe GlVertexBuffer(Span<float> data)
        {
            _gl = GlRenderer.Gl;
            _handle = _gl.CreateBuffer();

            fixed (void* dataPtr = data)
            {
                _gl.BufferData(
                    _bufferType,
                    (UIntPtr)(data.Length * sizeof(float)),
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
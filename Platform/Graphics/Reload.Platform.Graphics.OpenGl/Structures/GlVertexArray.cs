using Reload.Rendering.Buffers;
using Reload.Platform.Graphics.OpenGl.Properties;
using Silk.NET.OpenGL;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reload.Platform.Graphics.OpenGl.Structures
{
    internal class GlVertexArray : VertexArray
    {
        private GL _gl;
        private uint _handle;

        public GlVertexArray(GL api)
        {
            _gl = api;
            _handle = _gl.CreateVertexArray();
            VertexBuffers = new List<VertexBuffer>();
        }

        public override void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            _gl.BindVertexArray(_handle);
            indexBuffer.Bind();

            IndexBuffer = indexBuffer;
        }

        public unsafe override void AddVertexBuffer(VertexBuffer vertexBuffer)
        {
            _gl.BindVertexArray(_handle);
            vertexBuffer.Bind();

            Debug.Assert(vertexBuffer.Layout.Count > 0, Resources.VertexBufferHasNoLayout);

            uint index = 0;
            var layout = vertexBuffer.Layout;

            foreach (var element in layout)
            {
                _gl.EnableVertexAttribArray(index);
                _gl.VertexAttribPointer(
                    index,
                    element.GetComponentCount(),
                    GlUtils.ShaderDataTypeToGlBaseType(element.Type),
                    element.Normalized,
                    layout.Stride,
                    (void *)element.Offset);
                index++;
            }

            VertexBuffers.Add(vertexBuffer);
        }

        public override void Bind()
        {
            _gl.BindVertexArray(_handle);
        }

        public override void Unbind()
        {
            _gl.BindVertexArray(0);
        }

        public override void Dispose()
        {
        }
    }
}

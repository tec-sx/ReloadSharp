using Reload.Rendering.Buffers;
using Reload.Rendering.Model;
using Reload.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Reload.Rendering.Data
{
    public struct Renderer2DData: IDisposable
    {
        /// <summary>
        /// The maximum number of quads.
        /// </summary>
        public const uint MaxQuads = 20000;

        /// <summary>
        /// The maximum number of vertices.
        /// </summary>
        public const uint MaxVertices = MaxQuads * 4;

        /// <summary>
        /// The maximum number of indices.
        /// </summary>
        public const uint MaxIndices = MaxQuads * 6;

        /// <summary>
        /// The maximum number of texture slots.
        /// </summary>
        public const uint MaxTextureSlots = 32;

        /// <summary>
        /// The maximum number of lines.
        /// </summary>
        public const uint MaxLines = 10000;

        /// <summary>
        /// The maximum number of line vertices.
        /// </summary>
        public const uint MaxLineVertices = MaxLines * 2;

        /// <summary>
        /// The maximum number of line indices.
        /// </summary>
        public const uint MaxLineIndices = MaxLines * 6;

        /// <summary>
        /// Gets the quad vertex array.
        /// </summary>
        public VertexArray QuadVertexArray { get; set; }

        /// <summary>
        /// Gets the quad vertex buffer.
        /// </summary>
        public VertexBuffer QuadVertexBuffer { get; set; }

        /// <summary>
        /// Gets the texture shader.
        /// </summary>
        public ShaderProgram TextureShader { get; set; }

        /// <summary>
        /// Gets the white texture.
        /// </summary>
        public Texture2D WhiteTexture { get; set; }

        /// <summary>
        /// Gets the quad index count.
        /// </summary>
        public uint QuadIndexCount { get; set; }

        /// <summary>
        /// Gets or sets the quad vertex buffer base.
        /// </summary>
        public QuadVertex QuadVertexBufferBase { get; set; }

        /// <summary>
        /// Gets or sets the quad vertex buffer pointer.
        /// </summary>
        public QuadVertex QuadVertexBufferPtr { get; set; }

        /// <summary>
        /// Gets the texture slots.
        /// </summary>
        public List<Texture2D> TextureSlots { get; set; }

        /// <summary>
        /// Gets the texture slot index.
        /// </summary>
        public uint TextureSlotIndex { get; set; }

        /// <summary>
        /// Gets or sets the quad vertex positions.
        /// </summary>
        public Vector4 QuadVertexPositions { get; set; }

        /// <summary>
        /// Gets or sets the line vertex array.
        /// </summary>
        public VertexArray LineVertexArray { get; set; }

        /// <summary>
        /// Gets or sets the line vertex buffer.
        /// </summary>
        public VertexBuffer LineVertexBuffer { get; set; }

        /// <summary>
        /// Gets or sets the line shader.
        /// </summary>
        public ShaderProgram LineShader { get; set; }

        /// <summary>
        /// Gets or sets the line index count.
        /// </summary>
        public uint LineIndexCount { get; set; }

        /// <summary>
        /// Gets or sets the line vertex buffer base.
        /// </summary>
        public LineVertex LineVertexBufferBase { get; set; }

        /// <summary>
        /// Gets or sets the line vertex buffer ptr.
        /// </summary>
        public LineVertex LineVertexBufferPtr { get; set; }

        /// <summary>
        /// Gets or sets the camera view projection matrix.
        /// </summary>
        public Matrix4x4 CameraViewProjection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether depth test is active.
        /// </summary>
        public bool DepthTestIsActive { get; set; }

        /// <summary>
        /// Gets or sets the renderer statistics.
        /// </summary>
        public Statistics Statistics { get; set; }

        /// <summary>
        /// Disposes the resources.
        /// </summary>
        public void Dispose()
        {
            QuadVertexArray.Dispose();
            QuadVertexBuffer.Dispose();
            TextureShader.Dispose();

            LineVertexArray.Dispose();
            LineVertexBuffer.Dispose();
            LineShader.Dispose();
        }
    }
}

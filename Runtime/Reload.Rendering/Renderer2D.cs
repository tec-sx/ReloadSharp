using Reload.Core.Utils;
using Reload.Core.Utils.Extensions;
using Reload.Rendering.Data;
using Reload.Rendering.Buffers;
using System.Drawing;
using System.Numerics;
using Reload.Rendering.Model;
using Reload.Rendering.Shaders;

namespace Reload.Rendering
{
    /// <summary>
    /// The 2D Renderer.
    /// </summary>
    public static class Renderer2D
    {
        private static Renderer2DData _data;

        #region Methods

        /// <summary>
        /// Intializes the renderer.
        /// </summary>
        internal static void Initialize()
        {
            _data = new Renderer2DData();
            _data.QuadVertexArray = VertexArray.Create();

            float[] squareVertices =
            {
                -0.75f, -0.75f, 0.0f, /* Texture */ 0.0f, 0.0f,
                 0.75f, -0.75f, 0.0f, /* Texture */ 1.0f, 0.0f,
                 0.75f,  0.75f, 0.0f, /* Texture */ 1.0f, 1.0f,
                -0.75f,  0.75f, 0.0f, /* Texture */ 0.0f, 1.0f,
            };

            uint[] squareIndices = { 0, 1, 2, 2, 3, 0 };

            BufferLayoutCollection squareBufferLayout = new BufferLayoutCollection
            {
                new BufferElement(ShaderDataType.Float3, "a_Position"),
                new BufferElement(ShaderDataType.Float2, "a_TexCoord"),
            };

            VertexBuffer squareVB = VertexBuffer.Create(squareVertices, squareBufferLayout);
            IndexBuffer squareIB = IndexBuffer.Create(squareIndices);

            _data.QuadVertexArray.AddVertexBuffer(squareVB);
            _data.QuadVertexArray.SetIndexBuffer(squareIB);
            _data.TextureShader = ShaderProgram.Create("Renderer2D", null);
        }

        /// <summary>
        /// Shuts down the renderer and clears resources.
        /// </summary>
        public static void ShutDown()
        {
            _data.Dispose();
        }

        /// <summary>
        /// Prepares the renderer to render a new frame.
        /// </summary>
        /// <summary>
        public static void BeginScene(Camera.Camera camera)
        {
            _data.TextureShader.Bind();
            _data.TextureShader.SetMatrix4("u_ViewProjection", camera.ViewProjectionMatrix);
        }

        /// <summary>
        /// Ends rendering the frame.
        /// </summary>
        public static void EndScene()
        { }

        /// <summary>
        /// Draws a quad.
        /// </summary>
        /// <remarks>
        /// <param name="position">The position of the quad.</param>
        /// <param name="size">The size of the quad.</param>
        /// <param name="color">The color of the quad.</param>
        public static void DrawQuad(Vector2 position, Vector2 size, float rotation, Color color)
        {
            DrawQuad(new Vector3(position.X, position.Y, 0.0f), size, rotation, color);
        }

        /// <summary>
        /// Draws a quad.
        /// </summary>
        /// <remarks>
        /// This overload of the method uses <see cref="Vector3"/> for the position
        /// and can be used to layer multiple quads by setting different values
        /// on the Z axis.
        /// </remarks>
        /// <param name="position">The position of the quad.</param>
        /// <param name="size">The size of the quad.</param>
        /// <param name="color">The color of the quad.</param>
        public static void DrawQuad(Vector3 position, Vector2 size, float rotation, Color color)
        {
            Matrix4x4 transform = Matrix4x4.CreateTranslation(position)
                                * Matrix4x4.CreateRotationZ(ReloadMath.DegreesToRadians(rotation))
                                * Matrix4x4.CreateScale(size.X, size.Y, 1.0f);

            _data.TextureShader.SetVector4("u_Color", color.ToVector4());
            _data.TextureShader.SetMatrix4("u_Transform", transform);
            _data.QuadVertexArray.Bind();

            RenderCommand.DrawIndexed(_data.QuadVertexArray);
        }

        /// <summary>
        /// Draws a quad.
        /// </summary>
        /// <remarks>
        /// <param name="position">The position of the quad.</param>
        /// <param name="size">The size of the quad.</param>
        /// <param name="texture">The texture of the quad.</param>
        public static void DrawQuad(Vector2 position, Vector2 size, float rotation, Texture2D texture)
        {
            DrawQuad(new Vector3(position.X, position.Y, 0.0f), size, rotation, texture);
        }

        /// <summary>
        /// Draws a quad.
        /// </summary>
        /// <remarks>
        /// This overload of the method uses <see cref="Vector3"/> for the position
        /// and can be used to layer multiple quads by setting different values
        /// on the Z axis.
        /// </remarks>
        /// <param name="position">The position of the quad.</param>
        /// <param name="size">The size of the quad.</param>
        /// <param name="texture">The texture of the quad.</param>
        public static void DrawQuad(Vector3 position, Vector2 size, float rotation, Texture2D texture)
        {
            Matrix4x4 transform = Matrix4x4.CreateTranslation(position)
                                * Matrix4x4.CreateRotationZ(ReloadMath.DegreesToRadians(rotation))
                                * Matrix4x4.CreateScale(size.X, size.Y, 1.0f);
            texture.Bind();
            _data.TextureShader.SetInt("u_Texture", 0);
            _data.TextureShader.SetVector4("u_Color", Color.White.ToVector4());
            _data.TextureShader.SetMatrix4("u_Transform", transform);
            _data.QuadVertexArray.Bind();

            RenderCommand.DrawIndexed(_data.QuadVertexArray);
        }

        /// <summary>
        /// Draws a quad.
        /// </summary>
        /// <remarks>
        /// This overload of the method uses <see cref="Vector3"/> for the position
        /// and can be used to layer multiple quads by setting different values
        /// on the Z axis.
        /// </remarks>
        /// <param name="position">The position of the quad.</param>
        /// <param name="size">The size of the quad.</param>
        /// <param name="texture">The texture of the quad.</param>
        /// <param name="tint">The tint of the texture.</param> 
        public static void DrawQuad(Vector3 position, Vector2 size, float rotation, Texture2D texture, Color tint)
        {
            Matrix4x4 transform = Matrix4x4.CreateTranslation(position)
                                * Matrix4x4.CreateRotationZ(ReloadMath.DegreesToRadians(rotation))
                                * Matrix4x4.CreateScale(size.X, size.Y, 1.0f);

            texture.Bind();
            _data.TextureShader.SetInt("u_Texture", 0);
            _data.TextureShader.SetVector4("u_Color", tint.ToVector4());
            _data.TextureShader.SetMatrix4("u_Transform", transform);
            _data.QuadVertexArray.Bind();

            RenderCommand.DrawIndexed(_data.QuadVertexArray);
        }

        #endregion
    }
}

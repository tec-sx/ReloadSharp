using Reload.Editor.Properties;
using Reload.Platform.Graphics.OpenGl;
using Reload.Rendering;
using Reload.Rendering.Buffers;
using Silk.NET.OpenGL;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using System;
using System.IO;
using System.Reflection;

namespace Reload.Editor.Platform
{
    public class OpenGl
    {
        private OpenGLBackend _glContext;
        private Viewport _viewport;

        private uint _frameBuffer;
        private uint _frameBufferTexture;
        private uint _depthRenderBuffer;

        private VertexArray _offscreenVA;
        private VertexBuffer _offscreenVB;
        private IndexBuffer _offscreenIB;

        private ShaderProgram _offscreenShader;

        public void Initialize(Viewport viewport)
        {
            var spaceAss = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "SpaceVIL.dll"));
            var getProcMethod = spaceAss
                .GetType("A.b")?
                .GetMethod(
                "B",
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new[] { typeof(string) },
                null);

            if (getProcMethod == null)
            {
                throw new ApplicationException(Resources.GlfwGetProcAdderssError);
            }

            var getProcAddress =
                (Func<string, IntPtr>)Delegate.CreateDelegate(typeof(Func<string, IntPtr>), getProcMethod);

            _glContext = new OpenGLBackend(getProcAddress);
            _viewport = viewport;
            _offscreenShader = ShaderProgram.Create("offscreenTex", null);

            RenderCommand.Initialize();
        }

        internal void PrepareViewport()
        {
            RenderCommand.SetViewportSize(new System.Drawing.Size(_viewport.GetWidth(), _viewport.GetHeight()));
            RenderCommand.Clear();
        }

        internal unsafe void GenerateTexturedFrameBufferObject()
        {
            int width = _viewport.GetWidth();
            int height = _viewport.GetHeight();

            if (width <= 0 || height <= 0)
            {
                return;
            }

            //Frame buffer
            _frameBuffer = _glContext.Api.GenFramebuffer();
            _glContext.Api.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBuffer);

            //Texture buffer
            _frameBufferTexture = _glContext.Api.GenTexture();
            _glContext.Api.BindTexture(GLEnum.Texture2D, _frameBufferTexture);
            _glContext.Api.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, (uint)width, (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
            _glContext.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Nearest);
            _glContext.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);
            _glContext.Api.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _frameBufferTexture, 0);

            // Depth buffer
            _depthRenderBuffer = _glContext.Api.GenRenderbuffer();
            _glContext.Api.BindRenderbuffer(GLEnum.Renderbuffer, _depthRenderBuffer);
            _glContext.Api.RenderbufferStorage(GLEnum.Renderbuffer, GLEnum.DepthComponent, (uint)width, (uint)height);
            _glContext.Api.BindRenderbuffer(GLEnum.Renderbuffer, 0);

            _glContext.Api.FramebufferRenderbuffer(GLEnum.Framebuffer, GLEnum.DepthAttachment, GLEnum.Renderbuffer, _depthRenderBuffer);

            var frameBufferStatus = _glContext.Api.CheckFramebufferStatus(GLEnum.Framebuffer);

            if (frameBufferStatus != GLEnum.FramebufferComplete)
            {
                throw new ApplicationException(Resources.FrameBufferNotComplete);
            }
        }

        internal void UnbindFrameBufferObject()
        {
            _glContext.Api.BindFramebuffer(GLEnum.Framebuffer, 0);
        }

        internal void GenerateTextureBuffers()
        {
            float[] vboData = new float[]
            {
                //X    Y   U     V
                -1f,  1f, 0f, 0f, 1f, //x0
                -1f, -1f, 0f, 0f, 0f, //x1
                 1f, -1f, 0f, 1f, 0f, //x2
                 1f,  1f, 0f, 1f, 1f, //x3
            };

            uint[] iboData = new uint[]
            {
                0, 1, 2,
                2, 3, 0,
            };

            var textureVertexLayout = new BufferLayoutCollection
            {
                new BufferElement(ShaderDataType.Float3, "a_Position"),
                new BufferElement(ShaderDataType.Float2, "a_TexCoord")
            };

            _offscreenVB = VertexBuffer.Create(new Span<float>(vboData), textureVertexLayout);
            _offscreenIB = IndexBuffer.Create(new Span<uint>(iboData));
            _offscreenVA = VertexArray.Create();
            _offscreenVA.AddVertexBuffer(_offscreenVB);
            _offscreenVA.SetIndexBuffer(_offscreenIB);
        }

        internal void BindTexture()
        {
            _glContext.Api.BindTexture(GLEnum.Texture2D, _frameBufferTexture);
        }

        internal void RenderToScreen()
        {
            RenderService.SetGLLayerViewport(_viewport.GetHandler(), _viewport);

            // Draw to texture
            BindTexture();

            _offscreenShader.Bind();
            _offscreenShader.SetInt("tex", 0);
            _offscreenVA.Bind();
            RenderCommand.DrawIndexed(_offscreenVA);
        }

        internal void CleanUpResources()
        {
            _glContext.Api.DeleteFramebuffer(_frameBuffer);
            _glContext.Api.DeleteRenderbuffer(_depthRenderBuffer);
            _glContext.Api.DeleteTexture(_frameBufferTexture);

            _offscreenVB.Dispose();
            _offscreenIB.Dispose();
        }
    }
}

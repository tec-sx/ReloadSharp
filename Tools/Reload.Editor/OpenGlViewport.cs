using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using Reload.Editor.Scenes;
using Reload.Platform.Graphics.OpenGl;
using Reload.Rendering;
using Reload.Scenes;
using Reload.Editor.Properties;
using SpaceVIL;
using SpaceVIL.Common;
using Silk.NET.OpenGL;
using Reload.Rendering.Structures;

namespace Reload.Editor
{
    public class OpenGlViewport : Prototype, SpaceVIL.Core.IOpenGLLayer
    {
        private const double FramesPerSecond = 60.0f;
        
        private readonly Stopwatch _renderStopwatch;
        private readonly Stopwatch _updateStopwatch;
        private readonly Stopwatch _lifetimeStopwatch;
        private readonly double _renderPeriod;
        private readonly double _updatePeriod;

        private GlContext _glContext;
        private uint _frameBuffer;
        private uint _frameBufferTexture;
        private uint _depthRenderBuffer;
        
        private VertexArray _textureVA;
        private VertexBuffer _textureVB;
        private IndexBuffer _textureIB;

        private ShaderProgram _offscreenShader;

        private SceneMachine _sceneMachine;   
        private bool _isInitialized;
        
        public OpenGlViewport(SceneMachine sceneMachine)
        {
            _renderStopwatch = new Stopwatch();
            _updateStopwatch = new Stopwatch();
            _lifetimeStopwatch = new Stopwatch();
            _renderPeriod = 1 / FramesPerSecond;
            _updatePeriod = 1 / FramesPerSecond;

            _sceneMachine = sceneMachine;
        }

        public override void InitElements()
        {
            base.InitElements();
        }

        public void Initialize()
        {
            var spaceAss = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "SpaceVIL.dll"));
            var getProcMethod = spaceAss
                .GetType("A.b")?
                .GetMethod(
                "B", 
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new [] {typeof(string)},
                null);

            if (getProcMethod == null)
            {
                throw new ApplicationException(Resources.GlfwGetProcAdderssException);
            }
            
            var getProcAddress = 
                (Func<string, IntPtr>) Delegate.CreateDelegate(typeof(Func<string, IntPtr>), getProcMethod);
            
            _glContext = new GlContext(getProcAddress);
            
            SetBackground(0, 75, 75);
            SetSizePolicy(SpaceVIL.Core.SizePolicy.Expand, SpaceVIL.Core.SizePolicy.Expand);

            _renderStopwatch.Start();
            _updateStopwatch.Start();
            _lifetimeStopwatch.Start();

            _sceneMachine.AddScene<DefaultScene>();
            _sceneMachine.Run();

            _offscreenShader = ShaderProgram.Create("offscreenTex", null);
            RenderCommand.Initialize();

            _isInitialized = true;
        }

        public bool IsInitialized() => _isInitialized;

        public void Draw()
        {
            double updateDelta = _updateStopwatch.Elapsed.TotalSeconds;
            if (updateDelta > _updatePeriod)
            {
                _sceneMachine.UpdateActiveScene(updateDelta);
                _updateStopwatch.Restart();
            }

            Size viewportSize = new Size(GetWidth(), GetHeight());
            Point location = new Point(GetX(), GetY());

            GenerateTexturedFrameBufferObject();
            RenderCommand.SetViewportSizeAndLocation(location, viewportSize);
            RenderCommand.Clear();

            double renderDelta = _renderStopwatch.Elapsed.TotalSeconds;
            if (renderDelta >= _renderPeriod || WindowManager.GetVSyncValue() > 0)
            {
                _sceneMachine.RenderActiveScene(renderDelta);

                UnbindFrameBufferObject();
                GenerateTextureBuffers();
                RenderToScreen();
                CleanUpResources();

                _renderStopwatch.Restart();
            }

            Renderer.EndScene();
        }

        public void Free()
        {
        }

        public void Dispose()
        {
        }

        private unsafe void GenerateTexturedFrameBufferObject()
        {
            uint width = (uint)GetWidth();
            uint height = (uint)GetHeight();

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
            _glContext.Api.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
            _glContext.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Nearest);
            _glContext.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);
            _glContext.Api.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _frameBufferTexture, 0);

            // Depth buffer
            _depthRenderBuffer = _glContext.Api.GenRenderbuffer();
            _glContext.Api.BindRenderbuffer(GLEnum.Renderbuffer, _depthRenderBuffer);
            _glContext.Api.RenderbufferStorage(GLEnum.Renderbuffer, GLEnum.DepthComponent, (uint)GetWidth(), (uint)GetHeight());
            _glContext.Api.BindRenderbuffer(GLEnum.Renderbuffer, 0);

            _glContext.Api.FramebufferRenderbuffer(GLEnum.Framebuffer, GLEnum.DepthAttachment, GLEnum.Renderbuffer, _depthRenderBuffer);

            var frameBufferStatus = _glContext.Api.CheckFramebufferStatus(GLEnum.Framebuffer);
            
            if (frameBufferStatus != GLEnum.FramebufferComplete)
            {
                throw new ApplicationException("Frame buffer object fail.");
            }
        }

        private void UnbindFrameBufferObject()
        {
            _glContext.Api.BindFramebuffer(GLEnum.Framebuffer, 0);
        }

        private void GenerateTextureBuffers()
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

            var textureVertexLayout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "a_position"),
                new BufferElement(ShaderDataType.Float2, "a_texCoord")
            };

            _textureVB = VertexBuffer.Create(new Span<float>(vboData));
            _textureIB = IndexBuffer.Create(new Span<uint>(iboData));
            _textureVB.SetLayout(textureVertexLayout);
            _textureVA = VertexArray.Create();
            _textureVA.AddVertexBuffer(_textureVB);
            _textureVA.SetIndexBuffer(_textureIB);
        }

        private void BindTexture()
        {
            _glContext.Api.BindTexture(GLEnum.Texture2D, _frameBufferTexture);
        }

        private void RenderToScreen()
        {
            RenderService.SetGLLayerViewport(GetHandler(), this);

            // Draw to texture
            BindTexture();

            _offscreenShader.Use();
            _offscreenShader.SetUniform("tex", 0);
            _textureVA.Bind();
            RenderCommand.DrawIndexed(_textureVA);
        }

        private void CleanUpResources()
        {
            _glContext.Api.DeleteFramebuffer(_frameBuffer);
            _glContext.Api.DeleteRenderbuffer(_depthRenderBuffer);
            _glContext.Api.DeleteTexture(_frameBufferTexture);
            
            _textureVB.Dispose();
            _textureIB.Dispose();
        }
    }
}
using Reload.Core.Utils;
using Reload.Rendering;
using Silk.NET.OpenGL;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Reload.Platform.Graphics.OpenGl.Properties;

namespace Reload.Platform.Graphics.OpenGl
{
    /// <summary>
    /// The OpenGL renderer.
    /// </summary>
    internal class GlRenderer : RendererBackend
    {
        private readonly GL _gl;

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlRenderer"/> class.
        /// </summary>
        /// <param name="api">The api.</param>
        public unsafe GlRenderer(GL api)
        {
            _gl = api;

            Capabilities = new RendererBackendCapabilities(
                new string((sbyte*)_gl.GetString(GLEnum.Vendor)),
                new string((sbyte*)_gl.GetString(GLEnum.Renderer)),
                new string((sbyte*)_gl.GetString(GLEnum.Version)),
                _gl.GetInteger(GLEnum.MaxSamples),
                _gl.GetInteger(GLEnum.MaxTextureMaxAnisotropy),
                _gl.GetInteger(GLEnum.MaxCombinedTextureImageUnits));
        }

        /// <inheritdoc/>
        public unsafe override void Initialize()
        {
#if DEBUG
            _gl.Enable(GLEnum.DebugOutput);
            _gl.Enable(GLEnum.DebugOutputSynchronous);
            _gl.DebugMessageCallback(OpenGlDebugLog, null);
#endif

            uint vertexArrayObject = _gl.GenVertexArray();
            _gl.BindVertexArray(vertexArrayObject);

            _gl.Enable(EnableCap.DepthTest);
            //_gl.Enable(EnableCap.CullFace);
            _gl.Enable(EnableCap.TextureCubeMapSeamless);
            _gl.FrontFace(FrontFaceDirection.Ccw);

            _gl.Enable(EnableCap.Blend);
            // TODO: Test with BlendingFactor.Alpha for first argument
            _gl.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);

            _gl.Enable(EnableCap.Multisample);

            GLEnum error =_gl.GetError();

            while (error != GLEnum.NoError)
            {
                Logger.PrintError(error.ToString());
                error = _gl.GetError();
            }
        }

        /// <inheritdoc/>
        public override void Clear(Color color)
        {
            _gl.ClearColor(color);
            _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        }

        /// <inheritdoc/>
        public unsafe override void DrawIndexed(uint count, PrimitiveType type, bool depthTest = true)
        {
            if (!depthTest)
            {
                _gl.Disable(EnableCap.DepthTest);
            }

            _gl.DrawElements(GLEnum.Triangles, count, GLEnum.UnsignedInt, null);

            if (!depthTest)
            {
                _gl.Enable(EnableCap.DepthTest);
            }
        }

        /// <inheritdoc/>
        public override void SetLineThickness(float thickness)
        {
            _gl.LineWidth(thickness);
        }

        /// <summary>
        /// Logs OpenGl debug messages to the console.
        /// </summary>
        /// <param name="source">The error source.</param>
        /// <param name="typeIn">The error type.</param>
        /// <param name="id">The error id.</param>
        /// <param name="severity">The error severity.</param>
        /// <param name="length">The length of the message.</param>
        /// <param name="messagePtr">The message pointer.</param>
        /// <param name="userparam">The userparam.</param>
        private static void OpenGlDebugLog(
            GLEnum source,
            GLEnum typeIn,
            int id,
            GLEnum severity,
            int length,
            IntPtr messagePtr,
            IntPtr userparam)
        {
            if (severity == GLEnum.DebugSeverityNotification)
            {
                return;
            }

            string message = Marshal.PtrToStringAnsi(messagePtr);
            string type = typeIn.ToString().Substring(9);
            string formattedMessage = string.Format(Resources.GlDebugFormat, type, id, message);

            switch (severity)
            {
                case GLEnum.DebugSeverityHigh:
                    Logger.PrintError(formattedMessage);
                    break;
                case GLEnum.DebugSeverityMedium:
                    Logger.PrintWarning(formattedMessage);
                    break;
                case GLEnum.DebugSeverityLow:
                    Logger.PrintInfo(formattedMessage);
                    break;
                default:
                    break;
            };
        }

        /// <ineritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                if (disposing)
                {
                }
            }

            _isDisposed = false;
        }
    }
}

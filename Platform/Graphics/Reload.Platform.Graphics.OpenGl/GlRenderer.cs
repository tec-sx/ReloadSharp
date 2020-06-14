namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Rendering;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class GlRenderer : RendererApi
    {
        private GL _gl;

        public unsafe GlRenderer(GL api)
        {
            _gl = api;

#if DEBUG
            _gl.Enable(GLEnum.DebugOutput);
            _gl.Enable(GLEnum.DebugOutputSynchronous);
            _gl.DebugMessageCallback(OnDebug, null);
#endif
        }

        public override void SetViewport(Size size)
        {
            _gl.Viewport(size);
        }

        public override void Clear()
        {
            _gl.Clear((uint)(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit |
                ClearBufferMask.StencilBufferBit));
        }

        public override void SetClearColor(Color color)
        {
            _gl.ClearColor(color);
        }

        public unsafe override void DrawIndexed(VertexArray vertexArray)
        {
            _gl.DrawElements(
                GLEnum.Triangles,
                vertexArray.IndexBuffer.Count,
                GLEnum.UnsignedInt,
                null);
        }

        private static void OnDebug(
            GLEnum source,
            GLEnum type,
            int id,
            GLEnum severity,
            int length,
            IntPtr message,
            IntPtr userparam)
        {
            Console.WriteLine(
                Properties.Resources.GraphicsManager_OnDebug,
                severity.ToString().Substring(13),
                type.ToString().Substring(9),
                id,
                Marshal.PtrToStringAnsi(message));
        }
    }
}

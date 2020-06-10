namespace Reload.Rendering.Platform.OpenGl
{
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class GlRenderer : RendererApi
    {
        public static GL Gl { get; private set; }

        public GlRenderer(IWindow window)
        {
            Gl = GL.GetApi(window);
        }

        public override void SetViewport(Size size)
        {
            Gl.Viewport(size);
        }

        public override void Clear()
        {
            Gl.Clear(
                (uint)ClearBufferMask.ColorBufferBit |
                (uint)ClearBufferMask.DepthBufferBit);
        }

        public override void SetClearColor(Color color)
        {
            Gl.ClearColor(color);
        }

        public override void DrawIndexed(VertexArray vertexArray)
        {
            Gl.DrawElements(
                PrimitiveType.Triangles,
                vertexArray.IndexBuffer.Count,
                DrawElementsType.UnsignedInt,
                null);
        }

        public unsafe void SetupOpenGl()
        {
#if DEBUG
            Gl.Enable(GLEnum.DebugOutput);
            Gl.Enable(GLEnum.DebugOutputSynchronous);
            Gl.DebugMessageCallback(OnDebug, null);
#endif
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

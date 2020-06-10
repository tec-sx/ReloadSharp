namespace Reload.Rendering
{
    using Silk.NET.Windowing.Common;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using Reload.Rendering.Structures;

    public class RenderCommand
    {
        private static RendererApi _rendererAPI;

        internal static void Initialize(IWindow window)
        {
            _rendererAPI = RendererApi.Create(window);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetViewport(Size size)
        {
            _rendererAPI.SetViewport(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClearColor(Color color)
        {
            _rendererAPI.SetClearColor(color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear()
        {
            _rendererAPI.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawIndexed(VertexArray vertexArray)
        {
            _rendererAPI.DrawIndexed(vertexArray);
        }
    }
}

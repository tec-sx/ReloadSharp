namespace Reload.Rendering
{
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RenderCommand
    {
        private static RendererApi _rendererAPI;

        internal static void Initialize(RendererApi renderingApi) => _rendererAPI = renderingApi;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClearColor(Color color) => _rendererAPI.SetClearColor(color);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear() => _rendererAPI.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawIndexed(VertexArray vertexArray) => _rendererAPI.DrawIndexed(vertexArray);
    }
}

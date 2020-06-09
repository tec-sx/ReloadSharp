namespace Reload.Graphics.Contexts
{
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;

    public class OpenGl
    {
        public GL Api { get; }

        public OpenGl(IWindow window)
        {
            Api = GL.GetApi(window);
        }
    }
}

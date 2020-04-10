namespace Core.CoreSystem.Graphics.Device.OpenGl
{
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    
    public class OpenGlDevice : IGraphicsDevice
    {
        public GL Api { get; }
        public void Dispose()
        {
        }

        public void Initialize(IWindow window)
        {
        }

        public void WaitForIdle()
        {
        }
    }
}
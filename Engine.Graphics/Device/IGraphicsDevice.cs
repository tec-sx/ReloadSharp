namespace Engine.Graphics.Device
{
    using System;
    using Silk.NET.Windowing.Common;

    public interface IGraphicsDevice : IDisposable
    {
        void Initialize(IWindow window);
        void WaitForIdle();
    }
}

using System;
using Silk.NET.Windowing.Common;

namespace Core.CoreSystem.Graphics.Device
{
    public interface IGraphicsDevice : IDisposable
    {
        void Initialize(IWindow window);
        void WaitForIdle();
    }
}

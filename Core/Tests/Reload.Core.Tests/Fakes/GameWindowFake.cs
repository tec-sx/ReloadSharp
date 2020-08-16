using Reload.Core.Graphics;
using System;

namespace Reload.Core.Tests.Fakes
{
    internal class GameWindowFake : IGameWindow
    {
        public WindowBackendType BackendType => WindowBackendType.None;

        public IntPtr Handle => IntPtr.Zero;

        public int Width { get; set; }
        public int Height { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsFullScreen { get; set; }
        public bool IsVsyncOn { get; set; }

        public void Initialize()
        { }

        public void OnClose()
        { }

        public void OnRender(double deltaTime)
        { }

        public void OnStarting()
        { }

        public void OnUpdate(double deltaTime)
        { }

        public void ShutDown()
        { }
    }
}

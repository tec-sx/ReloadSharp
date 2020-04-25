namespace Engine.Graphics
{
    using Engine.Graphics.Device;
    using Silk.NET.Windowing.Common;
    using System;

    public interface IGraphicsManager : IDisposable
    {
        public IWindow Window { get; }
        public IGraphicsDevice Device { get; }

        void Initialize(DisplayConfiguration displayConfiguration);
        void CreateWindow();
        void CreateDevice();
    }
}

namespace Core.CoreSystem.Graphics.Device
{
    using System;
    using Silk.NET.Windowing.Common;

    internal abstract class GraphicsBackendBase<T> : IDisposable
    {
        public T Api { get; }
        public abstract void Initialize(IWindow window);
        public abstract void Dispose();
    }
}

namespace Core.CoreSystem.Graphics
{
    using System;
    using Silk.NET.Windowing.Common;

    internal interface IGraphicsBackend<out T> : IDisposable
    {
        public T Api { get; }
         void Initialize(IWindow window);
    }
}

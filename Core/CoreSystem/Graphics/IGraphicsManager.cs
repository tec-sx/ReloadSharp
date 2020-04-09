namespace Core.CoreSystem.Graphics
{
    using Device;
    using Silk.NET.Windowing.Common;

    internal interface IGraphicsManager
    {
        IWindow Window { get; }
        IGraphicsDevice Device { get; }

        void CreateWindow();
        void CreateDevice();
        void DisposeResources();
    }
}
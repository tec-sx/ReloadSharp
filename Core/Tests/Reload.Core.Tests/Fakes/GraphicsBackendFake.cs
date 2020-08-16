using Reload.Core.Graphics;

namespace Reload.Core.Tests.Fakes
{
    internal class GraphicsBackendFake : IGraphicsBackend
    {
        public GraphicsBackendType Type { get; init; } = GraphicsBackendType.None;

        public GraphicsAPIVersion Version => new GraphicsAPIVersion();

        public void Initialize()
        { }

        public void ShutDown()
        { }
    }
}

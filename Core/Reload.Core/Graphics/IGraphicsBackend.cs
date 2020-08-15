using Reload.Core.Game;

namespace Reload.Core.Graphics
{
    public interface IGraphicsBackend : ISubSystem
    {
        /// <summary>
        /// Gets the graphics backend type.
        /// </summary>
        GraphicsBackendType Type { get; }

        /// <summary>
        /// Gets the backend API version.
        /// </summary>
        GraphicsAPIVersion Version { get; }
    }
}

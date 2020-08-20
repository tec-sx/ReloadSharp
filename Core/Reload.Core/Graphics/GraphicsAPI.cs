using Reload.Core.Game;
using Reload.Core.Graphics.Rendering;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Graphics.Rendering.Shaders;
using Reload.Core.Graphics.Rendering.Textures;
using System;

namespace Reload.Core.Graphics
{
    /// <summary>
    /// The graphics API base.
    /// </summary>
    public abstract class GraphicsAPI : ISubSystem, IDisposable
    {
        #region Core factories

        /// <summary>
        /// Gets or sets the buffer service.
        /// </summary>
        internal protected static BufferFactory BufferFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the shaders service.
        /// </summary>
        internal protected static ShaderFactory ShaderFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the textures service.
        /// </summary>
        internal protected static TextureFactory TextureFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the renderer service.
        /// </summary>
        internal protected static RenderFactory RendererFactory { get; protected set; }

        #endregion

        /// <summary>
        /// Gets the graphics backend type.
        /// </summary>
        public GraphicsAPIType Type { get; protected init; }

        /// <summary>
        /// Gets the backend API version.
        /// </summary>
        public GraphicsAPIVersion Version { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsAPI"/> class.
        /// </summary>
        public GraphicsAPI()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsAPI"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="version">The version.</param>
        public GraphicsAPI(GraphicsAPIType type, GraphicsAPIVersion version)
        {
            Type = type;
            Version = version;
        }

        /// <inheritdoc/>
        public abstract void Initialize();

        /// <inheritdoc/>
        public abstract void ShutDown();

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

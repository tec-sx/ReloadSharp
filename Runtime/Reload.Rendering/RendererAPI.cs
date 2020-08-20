using Silk.NET.OpenGL;
using System;
using System.Drawing;


namespace Reload.Rendering
{
    public enum RendererBackendType
    {
        None = 0,
        OpenGl,
        Vulkan,
        DirectX
    }

    /// <summary>
    /// The renderer backend base class.
    /// </summary>
    public abstract class RendererAPI : IDisposable
    {
        /// <summary>
        /// Gets renderer's current api type.
        /// </summary>
        public RendererBackendType Type { get; }

        /// <summary>
        /// Gets the renderer capabilities.
        /// </summary>
        public RendererBackendCapabilities Capabilities { get; init; }

        /// <summary>
        /// Initializes the Renderer Api.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Clears the viewport to the color parameter value.
        /// </summary>
        /// <param name="color">The color to clear the viewport to.</param>
        public abstract void Clear(Color color);

        /// <summary>
        /// Draws the indexed vertex buffer.
        /// </summary>
        /// <param name="vertexArray">The vertex array.</param>
        public abstract void DrawIndexed(uint count, PrimitiveType type, bool depthTest = true);

        /// <summary>
        /// Sets the line thickness.
        /// </summary>
        /// <param name="thickness">The thickness of the lines.</param>
        public abstract void SetLineThickness(float thickness);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// Dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method 
        /// (its value is true) or from a finalizer
        protected abstract void Dispose(bool disposing);
    }
}

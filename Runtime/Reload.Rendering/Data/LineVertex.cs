using System.Drawing;
using System.Numerics;

namespace Reload.Rendering.Data
{
    public readonly struct LineVertex
    {
        /// <summary>
        /// Gets the vertex position.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the vertex color.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineVertex"/> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public LineVertex(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}

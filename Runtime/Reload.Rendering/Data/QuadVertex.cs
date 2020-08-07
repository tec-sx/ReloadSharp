using System.Drawing;
using System.Numerics;

namespace Reload.Rendering.Data
{
    public readonly struct QuadVertex
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
        /// Gets the vertex texture coordinate.
        /// </summary>
        public Vector2 TexCoord { get; }

        /// <summary>
        /// Gets the texrure index.
        /// </summary>
        public float TexIndex { get; }

        /// <summary>
        /// Gets the tiling factor.
        /// </summary>
        public float TilingFactor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadVertex"/> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="texCoord">The tex coord.</param>
        /// <param name="texIndex">The tex index.</param>
        /// <param name="tilingFactor">The tiling factor.</param>
        public QuadVertex(Vector3 position, Color color, Vector2 texCoord, float texIndex, float tilingFactor)
        {
            Position = position;
            Color = color;
            TexCoord = texCoord;
            TexIndex = texIndex;
            TilingFactor = tilingFactor;
        }
    }
}

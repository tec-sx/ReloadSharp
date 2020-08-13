using System;
using System.Drawing;
using System.Numerics;

namespace Reload.Rendering.Data
{
    public readonly struct LineVertex : IEquatable<LineVertex>
    {
        /// <summary>
        /// Gets the vertex position.
        /// </summary>
        public readonly Vector3 Position;
        /// <summary>
        /// Gets the vertex color.
        /// </summary>
        public readonly Color Color;

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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is LineVertex other && Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(LineVertex other)
        {
            return other != null
                && other.Position == Position
                && other.Color == Color;
        }

        /// <inheritdoc/>
        public static bool operator ==(LineVertex left, LineVertex right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(LineVertex left, LineVertex right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Color);
        }
    }
}

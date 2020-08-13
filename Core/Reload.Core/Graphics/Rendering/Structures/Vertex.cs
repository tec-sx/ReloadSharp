using System;
using System.Numerics;

namespace Reload.Core.Graphics.Rendering.Structures
{
    public readonly struct Vertex : IEquatable<Vertex>
    {
        /// <summary>
        /// Gets the vertex position.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the vertex normal.
        /// </summary>
        public Vector3 Normal { get; }

        /// <summary>
        /// Gets the vertex tangent.
        /// </summary>
        public Vector3 Tangent { get; }

        /// <summary>
        /// Gets the vertex bi-normal.
        /// </summary>
        public Vector3 BiNormal { get; }

        /// <summary>
        /// Gets the vertex texture coordinate.
        /// </summary>
        public Vector2 TexCoord { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> struct.
        /// </summary>
        /// <param name="position">The vertex position.</param>
        /// <param name="normal">The vertex normal.</param>
        /// <param name="tangent">The vertex tangent.</param>
        /// <param name="biNormal">The vertex bi-normal.</param>
        /// <param name="texCoord">The vertex texture coordinate.</param>
        public Vertex(
            Vector3 position, 
            Vector3 normal, 
            Vector3 tangent, 
            Vector3 biNormal, 
            Vector2 texCoord)
        {
            Position = position;
            Normal = normal;
            Tangent = tangent;
            BiNormal = biNormal;
            TexCoord = texCoord;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Normal, Tangent, BiNormal, TexCoord);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Vertex other && Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(Vertex other)
        {
            return other.Position == Position
                && other.Normal == Normal
                && other.Tangent == Tangent
                && other.BiNormal == BiNormal
                && other.TexCoord == TexCoord;
        }

        /// <inheritdoc/>
        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }
    }
}

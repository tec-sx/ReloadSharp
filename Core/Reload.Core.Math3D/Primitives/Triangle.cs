using Reload.Core.Math3D.Vertices;
using System;

namespace Reload.Core.Math3D.Primitives
{
    public readonly struct Triangle : IEquatable<Triangle>
    {
        public readonly Vertex V1;

        public readonly Vertex V2;

        public readonly Vertex V3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> struct.
        /// </summary>
        /// <param name="v1">First point.</param>
        /// <param name="v2">Second point.</param>
        /// <param name="v3">Third point.</param>
        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(V1, V2, V3);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((Triangle)obj);
        }

        /// <inheritdoc/>
        public bool Equals(Triangle other)
        {
            return other != null
                && other.V1 == V1
                && other.V2 == V2
                && other.V3 == V3;
        }

        /// <inheritdoc/>
        public static bool operator ==(Triangle left, Triangle right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Triangle left, Triangle right)
        {
            return !(left == right);
        }
    }
}

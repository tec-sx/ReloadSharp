using System;

namespace Reload.Core.Math3D.Vertices
{
    public readonly struct Index : IEquatable<Index>
    {
        public readonly uint V1;

        public readonly uint V2;

        public readonly uint V3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> struct.
        /// </summary>
        /// <param name="v1">First point.</param>
        /// <param name="v2">Second point.</param>
        /// <param name="v3">Third point.</param>
        public Index(uint v1, uint v2, uint v3) : this()
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
            return obj is Index other && Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(Index other)
        {
            return other.V1 == V1
                && other.V2 == V2
                && other.V3 == V3;
        }

        /// <inheritdoc/>
        public static bool operator ==(Index left, Index right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Index left, Index right)
        {
            return !(left == right);
        }
    }
}

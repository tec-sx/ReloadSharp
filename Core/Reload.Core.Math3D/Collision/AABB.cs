using System;
using System.Numerics;

namespace Reload.Core.Math3D.Collision
{
    public readonly struct AABB : IEquatable<AABB>
    {
        public readonly Vector3 Min;

        public readonly Vector3 Max;

        /// <summary>
        /// Initializes a new instance of the <see cref="AABB"/> class
        /// (Axis-aligned bounding box) used for collision detection.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        public AABB(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((AABB)obj);
        }

        /// <inheritdoc/>
        public bool Equals(AABB other)
        {
            return other != null 
                && Min.Equals(Min)
                && Max.Equals(Max);
        }

        /// <inheritdoc/>
        public static bool operator ==(AABB left, AABB right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(AABB left, AABB right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}

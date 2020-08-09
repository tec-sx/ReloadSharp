using System;
using System.Numerics;

namespace Reload.Core.Math3D.Vertices
{
    public struct BoneInfo : IEquatable<BoneInfo>
    {
        public Matrix4x4 BoneOffset;

        public Matrix4x4 FinalTransformation;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is BoneInfo && Equals((BoneInfo)obj);
        }

        /// <inheritdoc/>
        public bool Equals(BoneInfo other)
        {
            return other.BoneOffset == BoneOffset
                && other.FinalTransformation == FinalTransformation;
        }

        /// <inheritdoc/>
        public static bool operator ==(BoneInfo left, BoneInfo right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(BoneInfo left, BoneInfo right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(BoneOffset, FinalTransformation);
        }
    }
}

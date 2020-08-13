using System;
using System.Numerics;

namespace Reload.Core.Graphics.Rendering.Structures
{
    public readonly struct BoneInfo : IEquatable<BoneInfo>
    {
        /// <summary>
        /// Gets the bone offset.
        /// </summary>
        public Matrix4x4 BoneOffset { get; }

        /// <summary>
        /// Gets the final transformation.
        /// </summary>
        public Matrix4x4 FinalTransformation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoneInfo"/> struct.
        /// </summary>
        /// <param name="boneOffset">The bone offset.</param>
        /// <param name="finalTransformation">The final transformation.</param>
        public BoneInfo(Matrix4x4 boneOffset, Matrix4x4 finalTransformation)
        {
            BoneOffset = boneOffset;
            FinalTransformation = finalTransformation;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is BoneInfo other && Equals(other);
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

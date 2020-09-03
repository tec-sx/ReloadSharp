#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
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
